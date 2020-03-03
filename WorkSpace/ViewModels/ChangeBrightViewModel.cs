using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppEvents;
using OpenCvSharp;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ImageProcessor = Dlls;
namespace WorkSpace.ViewModels
{
    class ChangeBrightViewModel:BindableBase
    {
        public Mat ImageMat;
        
        private int _brightValue;

		public int BrightValue
		{
			get { return _brightValue; }
            set { SetProperty(ref _brightValue, value); }
        }

		private int _conValue;
        private EventAggregator _eventAggregator;

        public int ConValue
		{
			get { return _conValue; }
            set { SetProperty(ref _conValue, value); }
        }

        public ChangeBrightViewModel(EventAggregator eventAggregator)
        {
            BrightValue = 0;
            ConValue = 100;
            Confirm = new DelegateCommand(ConfirmExecute);
            Cancel = new DelegateCommand(CancelExecute);
            ConValueChanged =new DelegateCommand(ConValueChangedExecute);
            BrightValueChanged = new DelegateCommand(BrightValueChangedExecute);
            _eventAggregator = eventAggregator;
            // _imageMat = imageMat;
        }

        public DelegateCommand BrightValueChanged { get; private set; }

        private void BrightValueChangedExecute()
        {
            ImageMat = ImageProcessor.Mood.BriAndCon(ImageMat, ConValue, BrightValue);
            _eventAggregator.GetEvent<PreviewImageChange>().Publish(ImageMat);
        }


        public DelegateCommand ConValueChanged { get; private set; }

        private void ConValueChangedExecute()
        {
            ImageMat = ImageProcessor.Mood.BriAndCon(ImageMat, ConValue, BrightValue);
            _eventAggregator.GetEvent<PreviewImageChange>().Publish(ImageMat);
        }

        public DelegateCommand Confirm { get; private set; }

        private void ConfirmExecute()
        {
            
        }

        public DelegateCommand Cancel { get; private set; }

        private void CancelExecute()
        {

        }

	}
}
