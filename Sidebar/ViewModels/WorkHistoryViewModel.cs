using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppEvents;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Utils;
using WorkSpace.ViewModels;
using WorkSpace.Views;

namespace Sidebar.ViewModels
{
    public class WorkHistoryViewModel:BindableBase
    {
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;

        private ObservableCollection<Operation> _operations;

        public ObservableCollection<Operation> Operations
        {
            get { return _operations; }
            set { SetProperty(ref _operations,value); }
        }


        public Dictionary<int,ObservableCollection<Operation>> OperationHistories { get; set; }

        public WorkHistoryViewModel(IEventAggregator eventAggregator,IRegionManager regionManager)
        {
            // 变量初始化
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            OperationHistories = new Dictionary<int, ObservableCollection<Operation>>();
            Operations = new ObservableCollection<Operation>();


            // 命令初始化
            Back = new DelegateCommand(BackExecute);
            Forward = new DelegateCommand(ForwardExecute);
            Copy = new DelegateCommand(CopyExecute);
            Delete = new DelegateCommand(DeleteExecute);
            OperationSelected = new DelegateCommand<Operation>(OperationSelectedExecute);

            // 事件订阅
            _eventAggregator.GetEvent<TabChanged>().Subscribe(TabChanged);
            _eventAggregator.GetEvent<AddOperation>().Subscribe(AddOperationTo);
            
        }

        // 刷新显示当前Tab的操作历史
        private void RefreshOperation()
        {
            Operations = GetOperations();
            for (int i = 0; i < Operations.Count; i++)
            {
                Operations[i].CurrentId = i + 1;
            }
        }

        // 添加操作历史
        private void AddOperationTo(Operation op)
        {
            var ops = GetOperations();
            foreach (var operation in Operations)
            {
                operation.IsCurrent = false;
            }

            op.IsCurrent = true;
            ops.Add(op);
            RefreshOperation();
        }

        // 获得当前tab的操作历史
        private ObservableCollection<Operation> GetOperations()
        {
            var id = GetCurrentTabViewModelId();
            ObservableCollection<Operation> ops=null;
            if (OperationHistories.TryGetValue(id, out ops))
            {
                return ops;
            }
            else
            {
                ops = new ObservableCollection<Operation>();
                OperationHistories[id] = ops;
            }
            return ops;
        }


        // 获得当前工作区图片的ViewModelId
        private int GetCurrentTabViewModelId()
        {
            var first = _regionManager.Regions["ImageTabs"].ActiveViews.First() as ImageTab;
            return (first?.DataContext as ImageTabViewModel).Id;
            
        }

        
        private void TabChanged()
        {
            RefreshOperation();
        }

        public DelegateCommand<Operation> OperationSelected { get; set; }

        private void OperationSelectedExecute(Operation obj)
        {

        }

        private void ChangeOperation()
        {

        }


        // 后退
        public DelegateCommand Back { get; set; }

        private void BackExecute()
        {

        }

        // 前进
        public DelegateCommand Forward { get; set; }


        private void ForwardExecute()
        {

        }
        // 复制
        public DelegateCommand Copy { get; set; }

        private void CopyExecute()
        {

        }
        // 删除
        public DelegateCommand Delete { get; set; }

        private void DeleteExecute()
        {
            
        }
    }
}
