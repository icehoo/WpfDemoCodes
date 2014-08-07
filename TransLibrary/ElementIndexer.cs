using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TransLibrary
{
    class ElementIndexer : DependencyObject
    {
        public static readonly DependencyProperty PosProperty =
            DependencyProperty.RegisterAttached("Pos", typeof(int), typeof(ElementIndexer),
                                                new PropertyMetadata(-1));

        public static void SetPos(DependencyObject o, int pos)
        {
            o.SetValue(PosProperty, pos);
        }

        public static int GetPos(DependencyObject o)
        {
            return (int)(o.GetValue(PosProperty));
        }
    }
}
