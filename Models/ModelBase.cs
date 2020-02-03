using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.Models
{
    /// モデルのベースクラス
    public class ModelBase : INotifyPropertyChanged
    {
        /*****************************************************************************/
        /// INotifyPropertyChanged の実装
        /*****************************************************************************/
        /// イベントハンドラ
        public event PropertyChangedEventHandler PropertyChanged;        /// プロパティに変更があった場合に発生します

        /*****************************************************************************/
        /// プロパティ値の設定
        /*****************************************************************************/
        protected virtual bool SetValue<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(value, storage))
            {
                // 値が変化しない場合は何もしない
                return false;
            }

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /*****************************************************************************/
        /// プロパティ値変更通知
        /*****************************************************************************/
        public virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            RaiseEvent(new PropertyChangedEventArgs(propertyName));
        }

        /*****************************************************************************/
        /// プロパティ値変更通知
        /*****************************************************************************/
        public virtual void RaiseEvent(PropertyChangedEventArgs args)
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged(this, args);
        }
    }
}
