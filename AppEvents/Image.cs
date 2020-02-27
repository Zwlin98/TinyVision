using Prism.Events;
using Utils;

namespace AppEvents
{
    public class SaveImage:PubSubEvent
    {
    }

    public class CanSaveImage : PubSubEvent
    {

    }

    public class CanEditImage : PubSubEvent
    {

    }

    public class TabChanged : PubSubEvent
    {

    }

    public class AddOperation : PubSubEvent<Operation>
    {

    }

    public class OperationChanged : PubSubEvent<Operation>
    {
    
    }
}