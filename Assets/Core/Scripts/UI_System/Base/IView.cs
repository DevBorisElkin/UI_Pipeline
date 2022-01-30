using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_Pipeline
{
    public interface IView
    {
        public void Init();
        
        public void Bind(List<IView> nextPanels);

        public void BindOverlapped(IView dependsOn);

        public void Show();

        public void Hide(IView next);

        public void Overlap(IView overlapView);
    }
}

