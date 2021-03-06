using System;

namespace MaJiang.WebSocket.Core.Server
{
  internal class WebSocketServiceHost<TBehavior> : WebSocketServiceHost
    where TBehavior : WebSocketBehavior
  {
    #region Private Fields

    private Func<TBehavior>         _initializer;
    private Logger                  _logger;
    private string                  _path;
    private WebSocketSessionManager _sessions;

    #endregion

    #region Internal Constructors

    internal WebSocketServiceHost (string path, Func<TBehavior> initializer, Logger logger)
    {
      _path = path;
      _initializer = initializer;
      _logger = logger;
      _sessions = new WebSocketSessionManager (logger);
    }

    #endregion

    #region Public Properties

    public override bool KeepClean {
      get {
        return _sessions.KeepClean;
      }

      set {
        var msg = _sessions.State.CheckIfAvailable (true, false, false);
        if (msg != null) {
          _logger.Error (msg);
          return;
        }

        _sessions.KeepClean = value;
      }
    }

    public override string Path {
      get {
        return _path;
      }
    }

    public override WebSocketSessionManager Sessions {
      get {
        return _sessions;
      }
    }

    public override Type Type {
      get {
        return typeof (TBehavior);
      }
    }

    public override TimeSpan WaitTime {
      get {
        return _sessions.WaitTime;
      }

      set {
        var msg = _sessions.State.CheckIfAvailable (true, false, false) ??
                  value.CheckIfValidWaitTime ();

        if (msg != null) {
          _logger.Error (msg);
          return;
        }

        _sessions.WaitTime = value;
      }
    }

    #endregion

    #region Protected Methods

    protected override WebSocketBehavior CreateSession ()
    {
      return _initializer ();
    }

    #endregion
  }
}
