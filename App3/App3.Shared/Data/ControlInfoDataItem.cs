using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App3.Data
{
    public class ControlInfoDataItem
    {
        public ControlInfoDataItem(string uniqueId, string title, string subtitle, string imagePath, string imageIconPath, string badgeString, string description, string content, bool isNew, bool isUpdated, bool isPreview)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.ImageIconPath = imageIconPath;
            this.BadgeString = badgeString;
            this.Content = content;
            this.IsNew = isNew;
            this.IsUpdated = isUpdated;
            this.IsPreview = isPreview;
            this.Docs = new ObservableCollection<ControlInfoDocLink>();
            this.RelatedControls = new ObservableCollection<string>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string ImageIconPath { get; private set; }
        public string BadgeString { get; private set; }
        public string Content { get; private set; }
        public bool IsNew { get; private set; }
        public bool IsUpdated { get; private set; }
        public bool IsPreview { get; private set; }
        public ObservableCollection<ControlInfoDocLink> Docs { get; private set; }
        public ObservableCollection<string> RelatedControls { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
