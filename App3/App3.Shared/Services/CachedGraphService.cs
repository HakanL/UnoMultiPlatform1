using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App3.Data;

namespace App3.Services
{
	public interface ICachedGraphFileService
	{
		Task SaveRootIdAsync(string rootId);
		Task<string> GetRootId();
		Task<IEnumerable<OneDriveItem>> GetCachedFilesAsync(string pathId);
		Task SaveCachedFilesAsync(IEnumerable<OneDriveItem> children, string pathId);
	}

	public class CachedGraphFileService : ICachedGraphFileService
	{
		public async Task SaveRootIdAsync(string rootId)
		{
		}

		public async Task<string> GetRootId()
		{
			return "root";
		}

		public async Task<IEnumerable<OneDriveItem>> GetCachedFilesAsync(string pathId)
		{
			if (string.IsNullOrEmpty(pathId))
				return new OneDriveItem[0];

			throw new NotImplementedException();
		}

		public async Task SaveCachedFilesAsync(IEnumerable<OneDriveItem> children, string pathId)
		{
		}
	}
}
