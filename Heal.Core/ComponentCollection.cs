using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Heal.Core
{
    public class ComponentCollection : Collection<IGameComponent>
    {
        #region Private field.

        private List<IUpdateable> updateableComponents = new List<IUpdateable>();
        private List<IDrawable> drawableComponents = new List<IDrawable>();

        private List<IUpdateable> currentlyUpdatingComponents = new List<IUpdateable>();
        private List<IDrawable> currentlyDrawingComponents = new List<IDrawable>();

        private class UpdateOrderComparer : IComparer<IUpdateable>
        {
            // Fields
            public static readonly UpdateOrderComparer Default = new UpdateOrderComparer();

            // Methods
            public int Compare(IUpdateable x, IUpdateable y)
            {
                if ((x == null) && (y == null))
                {
                    return 0;
                }
                if (x != null)
                {
                    if (y == null)
                    {
                        return -1;
                    }
                    if (x.Equals(y))
                    {
                        return 0;
                    }
                    if (x.UpdateOrder < y.UpdateOrder)
                    {
                        return -1;
                    }
                }
                return 1;
            }
        }

        private class DrawOrderComparer : IComparer<IDrawable>
        {
            // Fields
            public static readonly DrawOrderComparer Default = new DrawOrderComparer();

            // Methods
            public int Compare(IDrawable x, IDrawable y)
            {
                if ((x == null) && (y == null))
                {
                    return 0;
                }
                if (x != null)
                {
                    if (y == null)
                    {
                        return -1;
                    }
                    if (x.Equals(y))
                    {
                        return 0;
                    }
                    if (x.DrawOrder < y.DrawOrder)
                    {
                        return -1;
                    }
                }
                return 1;
            }
        }

        #endregion

        #region Events and methods of Collection.

        // Events
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;

        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;

        // Methods
        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this.OnComponentRemoved(new GameComponentCollectionEventArgs(base[i]));
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, IGameComponent item)
        {
            if (base.IndexOf(item) != -1)
            {
                throw new ArgumentException("Item already exist.");
            }
            base.InsertItem(index, item);
            if (item != null)
            {
                this.OnComponentAdded(new GameComponentCollectionEventArgs(item));
            }
        }

        private void OnComponentAdded(GameComponentCollectionEventArgs eventArgs)
        {
            GameComponentAdded( this, eventArgs );
            if (this.ComponentAdded != null)
            {
                this.ComponentAdded(this, eventArgs);
            }
        }

        private void OnComponentRemoved(GameComponentCollectionEventArgs eventArgs)
        {
            GameComponentRemoved( this, eventArgs );
            if (this.ComponentRemoved != null)
            {
                this.ComponentRemoved(this, eventArgs);
            }
        }

        protected override void RemoveItem(int index)
        {
            IGameComponent gameComponent = base[index];
            if (gameComponent != null)
            {
                this.OnComponentRemoved(new GameComponentCollectionEventArgs(gameComponent));
            }
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, IGameComponent item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Game component management.

        private void GameComponentAdded(object sender, GameComponentCollectionEventArgs e)
        {
            e.GameComponent.Initialize();
            IUpdateable gameComponent = e.GameComponent as IUpdateable;
            if (gameComponent != null)
            {
                int index = this.updateableComponents.BinarySearch(gameComponent, UpdateOrderComparer.Default);
                if (index < 0)
                {
                    index = ~index;
                    while ((index < this.updateableComponents.Count) && (this.updateableComponents[index].UpdateOrder == gameComponent.UpdateOrder))
                    {
                        index++;
                    }
                    this.updateableComponents.Insert(index, gameComponent);
                    gameComponent.UpdateOrderChanged += new EventHandler(this.UpdateableUpdateOrderChanged);
                }
            }
            IDrawable item = e.GameComponent as IDrawable;
            if (item != null)
            {
                int num2 = this.drawableComponents.BinarySearch(item, DrawOrderComparer.Default);
                if (num2 < 0)
                {
                    num2 = ~num2;
                    while ((num2 < this.drawableComponents.Count) && (this.drawableComponents[num2].DrawOrder == item.DrawOrder))
                    {
                        num2++;
                    }
                    this.drawableComponents.Insert(num2, item);
                    item.DrawOrderChanged += new EventHandler(this.DrawableDrawOrderChanged);
                }
            }
        }

        private void GameComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            IUpdateable gameComponent = e.GameComponent as IUpdateable;
            if (gameComponent != null)
            {
                this.updateableComponents.Remove(gameComponent);
                gameComponent.UpdateOrderChanged -= new EventHandler(this.UpdateableUpdateOrderChanged);
            }
            IDrawable item = e.GameComponent as IDrawable;
            if (item != null)
            {
                this.drawableComponents.Remove(item);
                item.DrawOrderChanged -= new EventHandler(this.DrawableDrawOrderChanged);
            }
        }

        private void DrawableDrawOrderChanged(object sender, EventArgs e)
        {
            IDrawable item = sender as IDrawable;
            this.drawableComponents.Remove(item);
            int index = this.drawableComponents.BinarySearch(item, DrawOrderComparer.Default);
            if (index < 0)
            {
                index = ~index;
                while ((index < this.drawableComponents.Count) && (this.drawableComponents[index].DrawOrder == item.DrawOrder))
                {
                    index++;
                }
                this.drawableComponents.Insert(index, item);
            }
        }

        private void UpdateableUpdateOrderChanged(object sender, EventArgs e)
        {
            IUpdateable item = sender as IUpdateable;
            this.updateableComponents.Remove(item);
            int index = this.updateableComponents.BinarySearch(item, UpdateOrderComparer.Default);
            if (index < 0)
            {
                index = ~index;
                while ((index < this.updateableComponents.Count) && (this.updateableComponents[index].UpdateOrder == item.UpdateOrder))
                {
                    index++;
                }
                this.updateableComponents.Insert(index, item);
            }
        }

        #endregion

        #region Update & draw functions.
        
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < this.drawableComponents.Count; i++)
            {
                this.currentlyDrawingComponents.Add(this.drawableComponents[i]);
            }
            for (int j = 0; j < this.currentlyDrawingComponents.Count; j++)
            {
                IDrawable drawable = this.currentlyDrawingComponents[j];
                if (drawable.Visible)
                {
                    drawable.Draw(gameTime);
                }
            }
            this.currentlyDrawingComponents.Clear();
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.updateableComponents.Count; i++)
            {
                this.currentlyUpdatingComponents.Add(this.updateableComponents[i]);
            }
            for (int j = 0; j < this.currentlyUpdatingComponents.Count; j++)
            {
                IUpdateable updateable = this.currentlyUpdatingComponents[j];
                if (updateable.Enabled)
                {
                    updateable.Update(gameTime);
                }
            }
            this.currentlyUpdatingComponents.Clear();
        }

        #endregion

    }
}