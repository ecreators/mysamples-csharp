using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using mysamples.api.attribute;
using mysamples.Properties;

namespace mysamples.api.wpf.model
{
    public class UIModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected bool set<T>(ref T obj, T now, [CallerMemberName] string propertyName = null)
        {
            var changed = !Equals(obj, now);
            var explicitCall = GetType().GetProperty(propertyName ?? string.Empty)?.CustomAttributes.Any(a => a.AttributeType == typeof(PropertyForceChangeAttribute)) ?? false;
            if (changed || explicitCall)
            {
                obj = now;
                onPropertyChanged(propertyName);
            }
            return changed;
        }
        
        protected virtual void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}