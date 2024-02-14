using ReadToKidsTracker.Models;

namespace ReadToKidsTracker.TransfromModel;

public class TransformModel
{
    public static List<ReadSessionView> Transform(List<ReadSession> sessions)
    {
        var listToDb = new List<ReadSessionView>();
        foreach (var session in sessions)
        {
            var viewData = MaptoView(session);
            listToDb.Add(viewData);
        }
        return listToDb;
    }

    public static ReadSession MaptoDb(ReadSessionView view)
    {
        var newModel = new ReadSession();
        newModel.Id = view.Id;
        newModel.StartPage = view.StartPage;
        newModel.EndPage = view.EndPage;
        newModel.TotalPages = view.EndPage - view.StartPage;
        newModel.BookName = view.BookName;
        newModel.Duration = view.Duration;
        newModel.Comments = view.Comments;
        newModel.Date = DateTime.Parse(view.Date);

        return newModel;
    }

    public static ReadSessionView MaptoView(ReadSession dbModel)
    {
        var newViewModel = new ReadSessionView();
        newViewModel.Id = dbModel.Id;
        newViewModel.StartPage = dbModel.StartPage;
        newViewModel.EndPage = dbModel.EndPage;
        newViewModel.TotalPages = dbModel.TotalPages;
        newViewModel.BookName = dbModel.BookName;
        newViewModel.Duration = dbModel.Duration;
        newViewModel.Comments = dbModel.Comments;
        newViewModel.Date = dbModel.Date.ToShortDateString();

        return newViewModel;
    }

}
