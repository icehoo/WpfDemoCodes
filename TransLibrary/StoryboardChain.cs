using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace TransLibrary
{
    [System.Windows.Markup.ContentProperty("Animates")]
    public class StoryboardChain
    {
        private FrameworkElement _obj;
        private List<Storyboard> _Animates = new List<Storyboard>();
        public List<Storyboard> Animates
        {
            get
            {
                return _Animates;
            }
            set
            {
                _Animates = value;
            }
        }

        public void Begin(FrameworkElement containingObject)
        {
            _obj = containingObject;

            if (Animates.Count == 0)
            {
                return;
            }

            for (int i = 0; i < Animates.Count; ++i)
            {
                Storyboard refBoard = Animates[i];
                int h = (i + 1 == Animates.Count) ? -1 : i + 1;
                ElementIndexer.SetPos(refBoard, h);
                refBoard.Completed -= OnCurrentFinished;
                refBoard.Completed += new EventHandler(OnCurrentFinished);
            }
            Animates[0].Begin(containingObject);
        }

        protected void OnCurrentFinished(object sender, EventArgs e)
        {
            ClockGroup sender_cg = (ClockGroup)sender;
            Storyboard sender_sb = (Storyboard)sender_cg.Timeline;
            if (sender_sb == null)
            {
                return;
            }
            int nextPos = ElementIndexer.GetPos(sender_sb);
            if ((nextPos == -1) || (Animates.Count <= nextPos))
            {
                return;
            }
            Animates[nextPos].Begin(_obj);
        }
    }
}
