using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Windows.Forms;
using Environment = devDept.Eyeshot.Environment;

namespace hanee.ThreeD
{
    // Environment의 property grid helper
    public class PropertyGridHelper
    {
        Environment environment { get; set; }

        Model model { get; set; }

        private PropertyGrid propertyGrid { get; set; }

        public PropertyGridHelper(Model model, PropertyGrid propertyGrid)
        {
            this.environment = model;
            this.model = model;
            this.propertyGrid = propertyGrid;

            this.propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
        }
        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject is Entity)
            {
                environment.Entities.Regen();

                propertyGrid.Refresh();

                environment.Invalidate();
            }
        }

        public void UpdatePropertyGridControl(Object obj)
        {
            if (propertyGrid == null)
                return;

            propertyGrid.SelectedObject = obj;

            propertyGrid.Refresh();
        }
    }
}
