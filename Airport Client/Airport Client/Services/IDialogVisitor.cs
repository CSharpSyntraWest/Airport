using System;

namespace Voorbeeld_Popup_Window.Services
{
    public interface IDialogVisitor
    {
        object DynamicVisit(Object data);
    }
}
