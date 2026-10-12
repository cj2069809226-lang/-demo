using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Kongyou.Dialogue
{
    /// <summary>
    /// 统一的按键查询。
    ///
    /// 这个工程用的是新版 Input System（activeInputHandler = Input System Package），
    /// 所以不能直接写 Input.GetKeyDown —— 那会直接抛异常。
    /// 这里把新旧两套 API 包一层，其余代码只管看 KeyCode。
    /// </summary>
    public static class KeyInput
    {
        public static bool WasPressedThisFrame(KeyCode key)
        {
#if ENABLE_INPUT_SYSTEM
            var kb = Keyboard.current;
            if (kb == null) return false;

            switch (key)
            {
                case KeyCode.Space: return kb.spaceKey.wasPressedThisFrame;
                case KeyCode.Return: return kb.enterKey.wasPressedThisFrame;
                case KeyCode.KeypadEnter: return kb.numpadEnterKey.wasPressedThisFrame;
                case KeyCode.LeftControl: return kb.leftCtrlKey.wasPressedThisFrame;
                case KeyCode.RightControl: return kb.rightCtrlKey.wasPressedThisFrame;
                case KeyCode.Escape: return kb.escapeKey.wasPressedThisFrame;
                case KeyCode.Tab: return kb.tabKey.wasPressedThisFrame;
                case KeyCode.R: return kb.rKey.wasPressedThisFrame;
                case KeyCode.T: return kb.tKey.wasPressedThisFrame;
                case KeyCode.Y: return kb.yKey.wasPressedThisFrame;
            }
            return false;
#elif ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKeyDown(key);
#else
            return false;
#endif
        }

        public static bool IsHeld(KeyCode key)
        {
#if ENABLE_INPUT_SYSTEM
            var kb = Keyboard.current;
            if (kb == null) return false;

            switch (key)
            {
                case KeyCode.Space: return kb.spaceKey.isPressed;
                case KeyCode.Return: return kb.enterKey.isPressed;
                case KeyCode.LeftControl: return kb.leftCtrlKey.isPressed;
                case KeyCode.RightControl: return kb.rightCtrlKey.isPressed;
            }
            return false;
#elif ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKey(key);
#else
            return false;
#endif
        }
    }
}
