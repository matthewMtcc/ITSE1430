using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Collections.ObjectModel; //for collectiion


namespace GameManager
{

    public class MemoryGameDatabase : GameDatabase //,IotherInterface
    {

        public MemoryGameDatabase()
        {
            //var game = new Game();
            //game.Name = "DOOM";
            //game.Description = "Space Marine";
            //game.Price = 49.99M;

            //Object initializer
            //var game = new Game() {
            //    Name = "DOOM",
            //    Description = "Space Marine",
            //    Price = 49.99M
            //};
            //Add(game);

            //game = new Game() { Name = "Oblivion", Description = "Medieval", Price = 89.99M };
            //Add(game);

            //Add(new Game() {
            //    Name = "Fallout 76",
            //    Description = "Failed MMO",
            //    Price = 0.01M
            //});

            //Collection initializer
            var games = new []
                {
                    new Game() { Name = "DOOM", Description = "Space Marine", Price = 49.99M },
                    new Game() { Name = "Oblivion", Description = "Medieval", Price = 89.99M },
                    new Game() { Name = "Fallout 76", Description = "Failed MMO", Price = 0.01M }
                };

            foreach (var game in games)
                AddCore(game);
        }

        protected override Game AddCore( Game game )
        {
            game.Id = ++_nextId;
            _items.Add(Clone(game));

            return game;
        }

        protected override Game UpdateCore( int id, Game game )
        {
            var index = GetIndex(id);

            game.Id = id;
            var existing = _items[index];
            Clone(existing, game);

            return game;
        }

        protected override void DeleteCore( int id )
        {
            var index = GetIndex(id);
            if (index >= 0)
                _items.RemoveAt(index);
        }

        protected override Game GetCore( int id )
        {
            var index = GetIndex(id);
            if (index >= 0)
                return Clone(_items[index]);

            return null;
        }

        //public Game[] GetAll()
        //public Game[] GetAll()
        protected override IEnumerable<Game> GetAllCore()
        {
            //var temp = new List<Game>();
            //foreach (var item in _items)
            //    temp.Add(Clone(item));

            //return temp.ToArray();

            //if (_items.Count == 0)
            //    yield return null;

            //Use iterator
            foreach (var item in _items)
                yield return Clone(item);
        }

        private void Clone( Game target, Game source )
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Owned = source.Owned;
            target.Completed = source.Completed;
        }

        private Game Clone( Game game )
        {
            var newGame = new Game();
            Clone(newGame, game);

            return newGame;
        }
        private int GetIndex( int id )
        {
            for (var index = 0; index < _items.Count; ++index)
                if (_items[index]?.Id == id)
                    return index;

            return -1;
        }


        //Arrays are so 90s
        //private readonly Game[] _items = new Game[100];

        //ArrayLists are so 00s
        //private readonly ArrayList _items = new ArrayList();

        private readonly List<Game> _items = new List<Game>();
        //private readonly Collection<Game> _items = new Collection<Game>();
        //clear();
        //contains(); 

        private int _nextId = 0;

    }
}
