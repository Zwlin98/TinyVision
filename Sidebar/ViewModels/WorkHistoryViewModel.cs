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
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            OperationSelected = new DelegateCommand<Operation>(OperationSelectedExecute);
            Delete = new DelegateCommand(DeleteExecute);

            _eventAggregator.GetEvent<TabChanged>().Subscribe(TabChanged);
            Operations = new ObservableCollection<Operation>();


            OperationHistories = new Dictionary<int, ObservableCollection<Operation>>();
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


        public DelegateCommand Delete { get; set; }

        private void DeleteExecute()
        {
            
        }
    }
}
