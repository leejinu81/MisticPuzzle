using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lonely
{
    public class UIManager : IInitializable
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            _text.enabled = false;
        }

        #endregion Explicit Interface

        private readonly Text _text;
        private readonly string _escapeText = "You are Escape!!";
        private readonly string _dieText = "You Die!!";

        public UIManager(Text text)
        {
            _text = text;
        }

        public void Escape()
        {
            _text.enabled = true;
            _text.text = _escapeText;
        }

        public void Die()
        {
            _text.enabled = true;
            _text.color = Color.red;
            _text.text = _dieText;
        }
    }
}