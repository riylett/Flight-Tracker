using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project_1
{
    public interface IObserver
    {
        void Update(IDUpdateArgs args);
        void Update(ContactInfoUpdateArgs args);
        void Update(PositionUpdateArgs args);
    }

    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(IDUpdateArgs args);
        void NotifyObservers(ContactInfoUpdateArgs args);
        void NotifyObservers(PositionUpdateArgs args);
    }
}