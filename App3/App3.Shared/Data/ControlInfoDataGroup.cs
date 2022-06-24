using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App3.Data
{
    public class ControlInfoDataGroup
    {
        public ControlInfoDataGroup(string uniqueId, string title, string subtitle, string imagePath, string imageIconPath, string description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.ImageIconPath = imageIconPath;
            this.Items = new ObservableCollection<ControlInfoDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string ImageIconPath { get; private set; }
        public ObservableCollection<ControlInfoDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
