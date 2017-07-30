using mysamples.api.attribute;
using mysamples.api.wpf.model;

namespace mysamples.api.wpf.designer
{
    public class WPFDesignerModel : UIModel
    {
        public const string VISIBLE_UI_TEXT_PROPERTY_NAME = "VisibleUIText";

        private string visibleUIText;
        private object uiContent;
        private string uiErrors;

        public WPFDesignerModel()
        {
            UIErrors = TextNoErrors;
        }

        [PropertyForceChange]
        public string VisibleUIText
        {
            get => visibleUIText;
            set => set(ref visibleUIText, value);
        }

        public string TextNoErrors { get; set; } = "OK - Keine Fehler";

        [PropertyForceChange]
        public object UIContent
        {
            get => uiContent;
            set => set(ref uiContent, value);
        }

        public string UIErrors
        {
            get => uiErrors;
            set => set(ref uiErrors, value);
        }
    }
}