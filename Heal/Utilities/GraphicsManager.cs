using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heal.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Heal.Data;

namespace Heal.Utilities
{
    public class GraphicsManager : GameClassManager
    {
        private static GraphicsManager m_instance;
        private Dictionary<string, Effect> m_effects = new Dictionary<string, Effect>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, SpriteFont> m_fonts = new Dictionary<string, SpriteFont>(StringComparer.OrdinalIgnoreCase);
        private Stack<RenderTarget2D> m_target = new Stack<RenderTarget2D>();
        private GraphicsDevice m_device;
        public GraphicsManager()
        {
            m_instance = this;
        }
        public override void Initialize()
        {
            m_device = HealGame.Game.GraphicsDevice;
            m_batch = new SpriteBatch( HealGame.Game.GraphicsDevice );
            //return;
            m_renderTarget1 = new RenderTarget2D(m_device, 800, 600, false,
                                                  m_device.PresentationParameters.BackBufferFormat, DepthFormat.Depth24Stencil8);
            m_renderTarget2 = new RenderTarget2D(m_device, 800, 600, false, m_device.PresentationParameters.
                                                                             BackBufferFormat, DepthFormat.Depth24Stencil8);
            m_resloveTexture = new RenderTarget2D(m_device, 800, 600, false, m_device.PresentationParameters.
                                                                                BackBufferFormat, DepthFormat.Depth24Stencil8);

        }

        public static GraphicsManager GetInstance()
        {
            return m_instance;
        }

        public delegate void ItemDrawer(SpriteBatch batch);

        private SpriteBatch m_batch = new SpriteBatch(HealGame.Game.GraphicsDevice);

        private RenderTarget2D m_renderTarget1;

        private RenderTarget2D m_renderTarget2;

        private RenderTarget2D m_resloveTexture;

        private RenderTarget2D m_finallyTarget;

        public class EffectDrawParameters
        {
            public string Effect;
            public int Technique;
            public int Pass;
        }


        public void Begin(RenderTarget2D target)
        {
            Begin(target, Color.Transparent);
        }

        public void Begin(RenderTarget2D target, Color defaultColor)
        {
        //RenderTarget2D oldTarget = (RenderTarget2D)m_batch.GraphicsDevice.GetRenderTarget(0);
            var renderTargets = m_batch.GraphicsDevice.GetRenderTargets();
            if (renderTargets.Length == 0)
            {
                m_finallyTarget = null;
            }
            else
            {
                m_finallyTarget = (RenderTarget2D)renderTargets[0].RenderTarget;
            }
            m_target.Push( m_finallyTarget );
            m_finallyTarget = target;
            m_batch.GraphicsDevice.SetRenderTarget(m_finallyTarget);
            m_batch.GraphicsDevice.Clear(defaultColor);
        }

        public void End()
        {
            //m_batch.GraphicsDevice.SetRenderTarget(0, target);
            m_finallyTarget = m_target.Pop();
            m_batch.GraphicsDevice.SetRenderTarget(null);
        }

        internal SpriteFont Fonts(string fontName)
        {
            SpriteFont font;
            if (!m_fonts.TryGetValue(fontName, out font))
            {
                font = DataReader.Load<SpriteFont>("Fonts/" + fontName);
                m_fonts.Add(fontName, font);
            }
            return font;
        }

        internal EffectParameter Parameters(string effectName, string paramName)
        {
            Effect effect;
            if (!m_effects.TryGetValue(effectName, out effect))
            {
                effect = DataReader.Load<Effect>("Effect/" + effectName);
                m_effects.Add(effectName, effect);
            }
            return effect.Parameters[paramName];
        }

        public void Draw(ItemDrawer drawer, EffectDrawParameters[] parameters, Color defaultColor)
        {
            //m_renderTarget1 = m_finallyTarget;
            //m_device.SetRenderTarget( null);
            //m_device.SetRenderTarget(m_resloveTexture);
            var tgt = m_batch.GraphicsDevice.GetRenderTargets()[0].RenderTarget;
            m_batch.GraphicsDevice.SetRenderTarget( m_renderTarget1);
            m_batch.GraphicsDevice.Clear(defaultColor);
            m_batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend);
            drawer(m_batch);
            m_batch.End();
            //return;
            RenderTarget2D temp;

            foreach( EffectDrawParameters effectDrawParameterse in parameters )
            {
                DrawEffect(m_renderTarget1, m_renderTarget2, effectDrawParameterse);
                temp = m_renderTarget1;
                m_renderTarget1 = m_renderTarget2;
                m_renderTarget2 = temp;
            }
            m_batch.GraphicsDevice.SetRenderTarget(tgt);
        }

        public void Draw(ItemDrawer drawer, EffectDrawParameters param, Color color)
        {
            Draw( drawer, new[] {param}, color );
        }

        public void Draw(ItemDrawer drawer)
        {
            m_batch.GraphicsDevice.SetRenderTarget( m_finallyTarget);
            m_batch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend);
            drawer(m_batch);
            m_batch.End();
        }

        private void DrawEffect(RenderTarget2D source, RenderTarget2D target, EffectDrawParameters param)
        {
            Effect effect;
            if (!m_effects.TryGetValue(param.Effect, out effect))
            {
                // effect = DataReader.Load<Effect>("Effect/" + param.Effect);
                m_effects.Add(param.Effect, effect);
            }
            m_batch.GraphicsDevice.SetRenderTarget(target);
            //m_batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            // effect.CurrentTechnique = effect.Techniques[param.Technique];
            //effect.Begin();
            //effect.CurrentTechnique.Passes[param.Pass].Begin();
            //effect.CurrentTechnique.Passes[param.Pass].Apply();
            //m_batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, effect);
            m_batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);
            m_batch.Draw(source, new Rectangle(0, 0, 800, 600), Color.White);
            m_batch.End();
            //effect.CurrentTechnique.Passes[param.Pass].End();
            //effect.End();
        }

        //public void Draw( RenderTarget2D target2D )
        //{
        //    RenderTarget2D target = (RenderTarget2D) m_batch.GraphicsDevice.GetRenderTarget( 0 );
        //    m_batch.GraphicsDevice.SetRenderTarget( 0, target2D );
        //    m_batch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None);
        //    m_batch.GraphicsDevice.Clear(Color.TransparentBlack);
        //    foreach (Texture2D texture2D in m_list)
        //    {
        //        m_batch.Draw( texture2D, new Rectangle( 0, 0, 800, 600 ), Color.White );
        //    }
        //    m_batch.End();
        //    m_batch.GraphicsDevice.SetRenderTarget( 0, target );
        //    m_list.Clear();
        //}
    }
}
