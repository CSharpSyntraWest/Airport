
using System;
using Voorbeeld_Popup_Window.Model;

namespace Voorbeeld_Popup_Window.Services
{

    public class DialogVisitor : IDialogVisitor
    {
        /// <summary>
        /// The method which is called by ViewModel classes to instantiate and show the dialog 
        /// windows.  By dynamic member resolution, the correct private Visit method will
        /// be invoked based on the method signature.
        /// </summary>
        /// <param name="data">The object which the dialog window
        /// will manipulate.</param>
        /// <returns>The object argument as modified.</returns>
        public object DynamicVisit(Object data) => Visit((dynamic)data);

        // create overloaded Visit methods.  The correct one will
        // be called based on the method signature, when the DynamicVisit delegate 
        // is invoked.
        //
        // This decouples the data (argument) from the action (dialog) performed
        // on it.

        private Persoon Visit(Persoon p)
        {
            var dlg = new PersonDialog();
            dlg.DataContext = p;
            dlg.ShowDialog();
            return p;
        }



    }
}
