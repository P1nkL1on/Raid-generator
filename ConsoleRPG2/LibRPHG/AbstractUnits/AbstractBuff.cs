using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRPHG.AbstractUnits
{
    public interface Ibuff
    {
        string Name { get; }
        int TurnsLeft { get; }
        void ApplyFor(Abstraceunit who, Abstraceunit bywho);
        void Dissaply();
        void Tick();
        Abstraceunit BuffHost {get;}
    }

    public abstract class Abstractbuff : Ibuff
    {
        protected string _buffname;
        protected Abstraceunit _bufftarget;
        protected Abstraceunit _buffhost;
        protected int _turnsleft;

        public virtual string Name { get { return _buffname; } }
        public virtual string NameFull { get { return String.Format("\t{0} ( casted on {1} by {2})\n\t  ", Name, _bufftarget.NameFull, _buffhost.NameFull); } }
        public int TurnsLeft { get { return _turnsleft; } }
        public virtual void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            _bufftarget = who;
            _buffhost = bywho;
            LOGS.Add(String.Format("{0} recieved \"{1}\" buff from {2}", _bufftarget.NameFull, _buffname, _buffhost.NameFull));
        }
        public virtual void Dissaply()
        {
            LOGS.Add(String.Format("{0} loosed \"{1}\" buff from {2}", _bufftarget.NameFull, _buffname, _buffhost.NameFull));
            _bufftarget = _buffhost = null;
        }

        public virtual Abstraceunit BuffHost { get { return _buffhost; } }
        
        public virtual void Tick(){
            _turnsleft--;
        }
    }
}
