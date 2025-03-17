using AIScripts.Friendly.GOAP.Behaviours;
using CrashKonijn.Goap;

namespace AIScripts.Friendly.GOAP
{
    public interface IInjectable
    {
        public void Inject(DependencyInjector injector);
    }
}