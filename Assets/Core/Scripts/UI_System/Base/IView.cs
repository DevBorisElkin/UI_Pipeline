using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_Pipeline
{
    public interface IView
    {
        public void Bind(List<IView> nextPanels);

        public void Show();

        public void Hide(IView next);
    }
}

