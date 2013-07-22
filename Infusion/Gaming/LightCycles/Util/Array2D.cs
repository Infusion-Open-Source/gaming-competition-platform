using System;
using System.Collections.Generic;
using System.Drawing;

namespace Infusion.Gaming.LightCycles.Util
{
    /// <summary>
    /// Generic implementation of two dimensional array features
    /// </summary>
    /// <typeparam name="T">type of data in array</typeparam>
    public class Array2D<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the Array2D class.
        /// </summary>
        /// <param name="width"> The width of the array. </param>
        /// <param name="height"> The height of the array. </param>
        public Array2D(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.Width = width;
            this.Height = height;
            this.Items = new T[this.Width, this.Height];
        }

        /// <summary>
        /// Initializes a new instance of the Array2D class with initial data set.
        /// </summary>
        /// <param name="data">Initializing data. </param>
        public Array2D(T[,] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.Width = data.GetLength(0);
            this.Height = data.GetLength(1);
            this.Items = new T[this.Width, this.Height];
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this.Items[x, y] = data[x, y];
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the width of the array.
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets the height of the array.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets the items of array.
        /// </summary>
        public T[,] Items { get; protected set; }

        /// <summary>
        /// Get item on specified coordinate
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>item at specified coordinate</returns>
        public T this[int x, int y]
        {
            get
            {
                return this.Items[x, y];
            }

            set
            {
                this.Items[x, y] = value;
            }
        }

        /// <summary>
        /// Get item on specified coordinate
        /// </summary>
        /// <param name="p">coordinate point</param>
        /// <returns>item at specified coordinate</returns>
        public T this[Point p]
        {
            get
            {
                return this.Items[p.X, p.Y];
            }

            set
            {
                this.Items[p.X, p.Y] = value;
            }
        }
        
        /// <summary>
        /// Fill array space with objects made by external function
        /// </summary>
        /// <param name="fillFunction">builder function of objects to fill</param>
        public void Fill(Func<Point,T> fillFunction)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Items[x, y] = fillFunction.Invoke(new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Fill array space with objects made by external function depending on check function result
        /// </summary>
        /// <param name="checkFunction">check function to use</param>
        /// <param name="fillFunction">builder function of objects to fill</param>
        public void Fill(Func<Point, T, bool> checkFunction, Func<Point, T> fillFunction)
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (checkFunction.Invoke(new Point(x, y), this.Items[x, y]))
                    {
                        this.Items[x, y] = fillFunction.Invoke(new Point(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Scans array and returns all found objects of specified type
        /// </summary>
        public List<T2> FindObjects<T2>()
            where T2 : T
        {
            List<T2> results = new List<T2>();
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (this.Items[x, y] != default(T) && this.Items[x, y].GetType() == typeof(T2))
                    {
                        results.Add((T2)this.Items[x, y]);
                    }
                }
            }

            return results;
        }
    }
}
