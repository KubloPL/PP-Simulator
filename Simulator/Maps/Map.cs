using System;
using System.Drawing;
using System.Collections.Generic;
using Simulator;

namespace Simulator.Maps
{
    public abstract class Map
    {
        public int SizeX { get; }
        
        public int SizeY { get; }

        private readonly Rectangle bounds;
        
        protected Map(int sizeX, int sizeY)
        {
            if (sizeX < 5)
                throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow (X)");
            if (sizeY < 5)
                throw new ArgumentOutOfRangeException(nameof(sizeY), "Too short (Y)");

            SizeX = sizeX;
            SizeY = sizeY;

            bounds = new Rectangle(0, 0, sizeX - 1, sizeY - 1);
        }
        
        public virtual bool Exist(Point point) => bounds.Contains(point);

        public abstract Point Next(Point start, Direction direction);

        public abstract Point NextDiagonal(Point start, Direction direction);
        
        public abstract void Add(IMappable mappable, Point point); 
        
        public abstract void Remove(IMappable mappable, Point point);
        
        public abstract void Move(IMappable mappable, Point from, Point to); 
        
        public abstract IEnumerable<IMappable> At(Point point);
        
        public abstract IEnumerable<IMappable> At(int x, int y); 
        
        public abstract IEnumerable<IMappable> GetAllMappables();
    }
}