using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Miru.ViewModel
{
    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> to simplify models.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Multicast event for property change notifications.
        /// 속성값이 변경되었을때 발생됩니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// 속성값이 원래 값과 일치하는지 확인합니다.속성값이 일치하지 않으면 속성값을 설정합니다.
        /// </summary>
        /// <typeparam name="T">
        /// Type of the property.
        /// 속성값의 타입입니다.</typeparam>
        /// <param name="storage">
        /// Reference to a property with both getter and setter.
        /// </param>
        /// <param name="value">
        /// Desired value for the property.
        /// 원하는 속성의 값입니다.</param>
        /// <param name="propertyName">
        /// Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.
        /// 발생되기 위해서 사용되는 속성의 이름입니다. 
        /// 이 값은 선택 사항이며 <see cref="CallerMemberNameAttribute"/>를 지원하는 컴파일러에서 
        /// 호출될때 자동으로 제공될 수 있습니다.</param>
        /// <returns>
        /// True if the value was changed, false if the existing value matched the
        /// desired value.
        /// 값이 다르면 변경되고, 같으면 변경되지 않습니다.
        /// </returns>

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)

        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// 속성값이 변경되면 발생합니다.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.
        /// 발생되기 위해서 사용되는 속성의 이름입니다. 
        /// 이 값은 선택 사항이며 <see cref="CallerMemberNameAttribute"/>를 지원하는 컴파일러에서 
        /// 호출될때 자동으로 제공될 수 있습니다.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}