using GradeBook.UI.ViewModels;

namespace GradeBook.UI
{
    public class ViewModelLocator
    {
        public StudentViewModelCollection StudentViewModelCollection => ServiceProviderHelper.GetService<StudentViewModelCollection>();
    }
}
