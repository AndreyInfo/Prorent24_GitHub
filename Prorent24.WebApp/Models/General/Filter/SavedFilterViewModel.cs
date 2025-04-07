using Prorent24.Enum.General;

namespace Prorent24.WebApp.Models.General.Filter
{
    /// <summary>
    /// Filter View Model
    /// </summary>
    public class SavedFilterViewModel
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Module type
        /// </summary>
        public ModuleTypeEnum ModuleType { get; set; }

        /// <summary>
        /// Filter group
        /// </summary>
        public FilterGroupTypeEnum FilterGroupType { get; set; }

        /// <summary>
        /// Filter type
        /// </summary>
        public FilterTypeEnum FilterType { get; set; }

        /// <summary>
        /// Aggregate text from selected fields
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Value is FilterValue model
        /// </summary>
        public SavedFilterValueViewModel Value { get; set; }
    }
}
