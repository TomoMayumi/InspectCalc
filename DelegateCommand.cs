using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InspectCalc
{
    /// デリゲートコマンド
    class DelegateCommand : ICommand
    {
        /// フィールド
        private readonly Action<object> m_Execute;               /// コマンドの実処理メソッド
        private readonly Func<object, bool> m_CanExecute;        /// コマンドの実行可否判定メソッド

        /// コンストラクタ
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// コンストラクタ
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            m_Execute = execute;
            m_CanExecute = canExecute;
        }

#pragma warning disable 0067
        /*****************************************************************************/
        /// コマンドの実行可否変更イベント
        /*****************************************************************************/
        public virtual event EventHandler CanExecuteChanged;       
#pragma warning restore

        /*****************************************************************************/
        /// コマンドの実行可否判定メソッドを呼び出す
        /*****************************************************************************/
        public bool CanExecute(object parameter)
        {
            return m_CanExecute != null ? m_CanExecute(parameter) : true;
        }

        /*****************************************************************************/
        /// コマンドの実処理メソッドを呼び出す
        /*****************************************************************************/
        public void Execute(object parameter)
        {
            if (m_Execute != null)
            {
                m_Execute(parameter);
            }
        }
    }    
}
