using System;
using CI.Common.Logging;

namespace CI.UI.Commands
{

    /// <summary>
    /// ICommandAspect
    /// </summary>
    public interface ICommandAspect
    {
        /// <summary>
        /// Befores this instance.
        /// </summary>
        void Before();

        /// <summary>
        /// Afters this instance.
        /// </summary>
        void After();

        void Error();

        /// <summary>
        /// Withes the specified aspect.
        /// </summary>
        /// <param name="aspect">The aspect.</param>
        /// <returns></returns>
        ICommandAspect With(ICommandAspect aspect);

    }

    /// <summary>
    /// Command AspectBase class
    /// </summary>
    public abstract class CommandAspectBase : ICommandAspect
    {

        private ICommandAspect _next;

        protected ILogger Logger { get; }

        protected CommandAspectBase(ILogger logger)
        {
            Logger = logger;
        }

        public virtual void Before()
        {
            _next.DoBefore(Logger);
        }

        public virtual void After()
        {
            _next.DoAfter(Logger);
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        public virtual void Error()
        {
            _next.DoError(Logger);
        }

        /// <summary>
        /// Withes the specified aspect.
        /// </summary>
        /// <param name="aspect">The aspect.</param>
        public ICommandAspect With(ICommandAspect aspect)
        {
            if (_next == null)
                _next = aspect;
            else
                _next.With(aspect);
            return this;
        }

    }

    public static class CommandAspectHelper
    {
        public static void DoAfter(this ICommandAspect aspect, ILogger logger)
        {
            try
            {
                aspect?.After();
            }
            catch (Exception ex)
            {
                logger?.Warning(ex);
            }
        }
        public static void DoBefore(this ICommandAspect aspect, ILogger logger)
        {
            try
            {
                aspect?.Before();
            }
            catch (Exception ex)
            {
                logger?.Warning(ex);
            }
        }
        public static void DoError(this ICommandAspect aspect, ILogger logger)
        {
            try
            {
                aspect?.Error();
            }
            catch (Exception ex)
            {
                logger?.Warning(ex);
            }
        }
    }
}
