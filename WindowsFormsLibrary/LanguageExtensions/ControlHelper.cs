namespace WindowsFormsLibrary.LanguageExtensions;

/// <summary>
/// TestFormControlHelper.ControlInvoke(listView1, () => listView1.Items.Add("Test"));
/// </summary>
public class ControlHelper
{
    delegate void UniversalVoidDelegate();

    /// <summary>
    /// Call form control action from different thread
    /// </summary>
    public static void ControlInvoke(Control control, Action function)
    {
        if (control.IsDisposed || control.Disposing)
        {
            return;
        }

        if (control.InvokeRequired)
        {
            control.Invoke(new UniversalVoidDelegate(() => ControlInvoke(control, function)));
            return;
        }

        function();
    }
}