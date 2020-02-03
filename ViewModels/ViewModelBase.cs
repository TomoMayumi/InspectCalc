using InspectCalc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InspectCalc.ViewModels
{
    /// ViewModelのベースクラス
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// フィールド
        private ModelBase m_Model;        /// モデル

        /// コンストラクタ
        protected ViewModelBase(ModelBase model)
        {
            m_Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        /// デストラクタ
        ~ViewModelBase()
        {
            Model.PropertyChanged -= Model_PropertyChanged;
        }

        /// プロパティ
        public ModelBase Model { get { return m_Model; } set { SetValue(ref m_Model, value); } }        /// モデル

        /*****************************************************************************/
        /// モデルからのプロパティ変更通知イベントハンドラ
        /*****************************************************************************/        
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
            ModelOnPropertyChanged(arg.PropertyName);
        }

        /*****************************************************************************/
        /// モデルからのプロパティ変更通知イベントハンドラ（抽象メソッド）
        /*****************************************************************************/
        protected virtual void ModelOnPropertyChanged(string propertyName)
        {

        }

        /*****************************************************************************/
        /// INotifyPropertyChanged の実装
        /*****************************************************************************/
        /// イベント
        public event PropertyChangedEventHandler PropertyChanged;        /// プロパティに変更があった場合に発生します

        /*****************************************************************************/
        /// プロパティ値設定
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
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            RaiseEvent(new PropertyChangedEventArgs(propertyName));
        }

        /*****************************************************************************/
        /// プロパティ値変更通知
        /*****************************************************************************/
        public virtual void RaiseEvent(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }

    }
}
