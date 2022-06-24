using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Data
{
    public sealed class ControlInfoDataSource
    {
        private static readonly object _lock = new object();

        #region Singleton

        private static ControlInfoDataSource _instance;

        public static ControlInfoDataSource Instance
        {
            get
            {
                return _instance;
            }
        }

        static ControlInfoDataSource()
        {
            _instance = new ControlInfoDataSource();
        }

        private ControlInfoDataSource() { }

        #endregion

        private IList<ControlInfoDataGroup> _groups = new List<ControlInfoDataGroup>();
        public IList<ControlInfoDataGroup> Groups
        {
            get { return this._groups; }
        }

        public async Task<IEnumerable<ControlInfoDataGroup>> GetGroupsAsync()
        {
            await _instance.GetControlInfoDataAsync();

            return _instance.Groups;
        }

        public async Task<ControlInfoDataGroup> GetGroupAsync(string uniqueId)
        {
            await _instance.GetControlInfoDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _instance.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public async Task<ControlInfoDataItem> GetItemAsync(string uniqueId)
        {
            await _instance.GetControlInfoDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _instance.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() > 0) return matches.First();
            return null;
        }

        public async Task<ControlInfoDataGroup> GetGroupFromItemAsync(string uniqueId)
        {
            await _instance.GetControlInfoDataAsync();
            var matches = _instance.Groups.Where((group) => group.Items.FirstOrDefault(item => item.UniqueId.Equals(uniqueId)) != null);
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetControlInfoDataAsync()
        {
            lock (_lock)
            {
                if (this.Groups.Count() != 0)
                {
                    return;
                }
            }

            lock (_lock)
            {
                ControlInfoDataGroup group = new ControlInfoDataGroup("NewControls", "Home", "", "", "", "");

                var item = new ControlInfoDataItem("AnimatedIcon", "AnimatedIcon", "An element that displays and controls an icon that animates when the user interacts with the control.",
                    "ms-appx:///Assets/ControlImages/AnimatedIcon.png","ms-appx:///Assets/ControlIcons/AnimationsIcon.png", "New",
                    "An element that displays and controls an icon that animates when the user interacts with the control.", "<p>Look at the <i>AnimatedIconPage.xaml</i> and <i>AnimatedIconPage.xaml.cs</i> files in Visual Studio to see the full code for this page.</p>",
                    true, false, false);
                group.Items.Add(item);

                if (!Groups.Any(g => g.Title == group.Title))
                {
                    Groups.Add(group);
                }
            }



            /*Uri dataUri = new Uri("ms-appx:///DataModel/ControlInfoData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);

            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            lock (_lock)
            {
                foreach (JsonValue groupValue in jsonArray)
                {
                    JsonObject groupObject = groupValue.GetObject();
                    ControlInfoDataGroup group = new ControlInfoDataGroup(groupObject["UniqueId"].GetString(),
                                                                          groupObject["Title"].GetString(),
                                                                          groupObject["Subtitle"].GetString(),
                                                                          groupObject["ImagePath"].GetString(),
                                                                          groupObject["ImageIconPath"].GetString(),
                                                                          groupObject["Description"].GetString());

                    foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                    {
                        JsonObject itemObject = itemValue.GetObject();

                        string badgeString = null;

                        bool isNew = itemObject.ContainsKey("IsNew") ? itemObject["IsNew"].GetBoolean() : false;
                        bool isUpdated = itemObject.ContainsKey("IsUpdated") ? itemObject["IsUpdated"].GetBoolean() : false;
                        bool isPreview = itemObject.ContainsKey("IsPreview") ? itemObject["IsPreview"].GetBoolean() : false;

                        if (isNew)
                        {
                            badgeString = "New";
                        }
                        else if (isUpdated)
                        {
                            badgeString = "Updated";
                        }
                        else if (isPreview)
                        {
                            badgeString = "Preview";
                        }

                        var item = new ControlInfoDataItem(itemObject["UniqueId"].GetString(),
                                                                itemObject["Title"].GetString(),
                                                                itemObject["Subtitle"].GetString(),
                                                                itemObject["ImagePath"].GetString(),
                                                                itemObject["ImageIconPath"].GetString(),
                                                                badgeString,
                                                                itemObject["Description"].GetString(),
                                                                itemObject["Content"].GetString(),
                                                                isNew,
                                                                isUpdated,
                                                                isPreview);

                        if (itemObject.ContainsKey("Docs"))
                        {
                            foreach (JsonValue docValue in itemObject["Docs"].GetArray())
                            {
                                JsonObject docObject = docValue.GetObject();
                                item.Docs.Add(new ControlInfoDocLink(docObject["Title"].GetString(), docObject["Uri"].GetString()));
                            }
                        }

                        if (itemObject.ContainsKey("RelatedControls"))
                        {
                            foreach (JsonValue relatedControlValue in itemObject["RelatedControls"].GetArray())
                            {
                                item.RelatedControls.Add(relatedControlValue.GetString());
                            }
                        }

                        group.Items.Add(item);
                    }

                    if (!Groups.Any(g => g.Title == group.Title))
                    {
                        Groups.Add(group);
                    }
                }
            }*/
        }
    }
}
