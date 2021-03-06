using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using App3.Data;
using Windows.Networking.Connectivity;

namespace App3.Services
{
    public interface IGraphFileService
    {
        Task<IEnumerable<OneDriveItem>> GetRootFilesAsync(Action<IEnumerable<OneDriveItem>, bool> cachedCallback = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<OneDriveItem>> GetFilesAsync(string id, Action<IEnumerable<OneDriveItem>, bool> cachedCallback = null, CancellationToken cancellationToken = default);
    }

    public class GraphFileService : IGraphFileService
    {
#if DEBUG
        const int ApiDelayInMilliseconds = 5000;
#endif

        //readonly GraphServiceClient graphClient;
        readonly INetworkConnectivityService networkConnectivity;
        readonly ILogger logger;
        readonly ICachedGraphFileService cachedService;
        public GraphFileService(
            ICachedGraphFileService cachedService,
            INetworkConnectivityService networkConnectivity, 
            ILogger<GraphFileService> logger)
        {
            this.cachedService = cachedService;
            this.networkConnectivity = networkConnectivity;
            this.logger = logger;

#if __WASM__
            var httpClient = new HttpClient(new Uno.UI.Wasm.WasmHttpHandler());
#else
            var httpClient = new HttpClient();
#endif

            cachedService = new CachedGraphFileService();
        }

        public async Task<IEnumerable<OneDriveItem>> GetRootFilesAsync(Action<IEnumerable<OneDriveItem>, bool> cachedCallback = null, CancellationToken cancellationToken = default)
        {
            return new List<OneDriveItem>();

            /*var rootPathId = await cachedService.GetRootId();
            if (networkConnectivity.Connectivity == NetworkConnectivityLevel.InternetAccess)
            {
                try
                {
                    var rootNode = await graphClient.Me.Drive.Root
                        .Request()
                        .GetAsync(cancellationToken);

                    if (rootNode == null || string.IsNullOrEmpty(rootNode.Id))
                        throw new KeyNotFoundException("Unable to find OneDrive Root Folder");

                    await cachedService.SaveRootIdAsync(rootNode.Id);
                    rootPathId = rootNode.Id;
                }
                catch (TaskCanceledException ex)
                {
                    throw ex;
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogWarning("Unable to retrieve data from Graph API, it may not exist or there could be a connection issue");
                    logger.LogWarning(ex, ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    logger.LogWarning("Unable to retrieve root OneDrive folder, attempting to use local data instead");
                    logger.LogWarning(ex, ex.Message);
                }
            }

            return await GetFilesAsync(rootPathId, cachedCallback, cancellationToken);*/
        }

        public async Task<IEnumerable<OneDriveItem>> GetFilesAsync(string id, Action<IEnumerable<OneDriveItem>, bool> cachedCallback = null, CancellationToken cancellationToken = default)
        {
            return new List<OneDriveItem>();
/*            if (cachedCallback != null)
            {
                var cachedChildren = await cachedService.GetCachedFilesAsync(id);
                cachedCallback(cachedChildren, true);
            }

            // If the response is null that means we couldn't retrieve data
            // due to no internet connectivity
            if (networkConnectivity.Connectivity != NetworkConnectivityLevel.InternetAccess)
                return null;

            cancellationToken.ThrowIfCancellationRequested();

#if DEBUG
            await Task.Delay(ApiDelayInMilliseconds, cancellationToken);
#endif

            var oneDriveItems = (await graphClient.Me.Drive.Items[id].Children
                .Request()
                .Expand("thumbnails")
                .GetAsync(cancellationToken))
                .ToArray();

            var childrenTable = oneDriveItems
                .Select(driveItem => new OneDriveItem
                {
                    Id = driveItem.Id,
                    Name = driveItem.Name,
                    Path = driveItem.ParentReference.Path,
                    PathId = driveItem.ParentReference.Id,
                    FileSize = $"{driveItem.Size}",
                    Modified = driveItem.LastModifiedDateTime.HasValue ?
                        driveItem.LastModifiedDateTime.Value.LocalDateTime : DateTime.Now,
                    Type = driveItem.Folder != null ? OneDriveItemType.Folder : OneDriveItemType.File
                    //ModifiedBy = driveItem.LastModifiedByUser.DisplayName,
                    //Sharing = ""
                })
                .OrderByDescending(item => item.Type)
                .ThenBy(item => item.Name)
                .ToDictionary(x => x.Id);

            cancellationToken.ThrowIfCancellationRequested();

            if (cachedCallback != null)
                cachedCallback(childrenTable.Select(x => x.Value), false);

            var children = childrenTable.Select(x => x.Value).ToArray();
            await cachedService.SaveCachedFilesAsync(children, id);

            for (int index = 0; index < oneDriveItems.Length; index++)
            {
                var currentItem = oneDriveItems[index];
                var thumbnails = currentItem.Thumbnails?.FirstOrDefault();
                if (thumbnails == null || !childrenTable.ContainsKey(currentItem.Id))
                    continue;

                var url = thumbnails.Medium.Url;

                var client = new HttpClient();
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    continue;

                var imagesFolder = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "thumbnails");
                var name = $"{currentItem.Id}.jpeg";
                var localFilePath = Path.Combine(imagesFolder, name);

                try
                {
                    if (!System.IO.Directory.Exists(imagesFolder))
                        System.IO.Directory.CreateDirectory(imagesFolder);

                    if (System.IO.File.Exists(localFilePath))
                        System.IO.File.Delete(localFilePath);

#if HAS_UNO_SKIA_WPF
                    System.IO.File.WriteAllBytes(localFilePath, await response.Content.ReadAsByteArrayAsync());
#else
                    await System.IO.File.WriteAllBytesAsync(localFilePath, await response.Content.ReadAsByteArrayAsync(), cancellationToken);
#endif
                    childrenTable[currentItem.Id].ThumbnailPath = localFilePath;

                    if (cachedCallback != null)
                        cachedCallback(childrenTable.Select(x => x.Value), false);

                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (TaskCanceledException ex)
                {
                    logger.LogWarning(ex, ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }

            return children;*/
        }
    }
}
