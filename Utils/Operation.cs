using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using OpenCvSharp;
using Prism.Mvvm;

namespace Utils
{
    public class Operation:BindableBase
    {
        public int Id { get; private set; }

        private int _currentID;

        public int CurrentId
        {
            get { return _currentID; }
            set { SetProperty(ref _currentID, value); }
        }


        public string Description { get; private set; }

        private bool _isCurrent;

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set { SetProperty(ref _isCurrent, value); }
        }


        public Mat ImageMat { get; private set; }

        private static int _id=1;

        public Operation(string description,Mat imageMat=null)
        {
            Id = _id++;
            Description = description;
            ImageMat = imageMat;
        }
    }
}
