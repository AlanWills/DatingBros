using System;
using Celeste.Input;
using Celeste.Narrative;
using Celeste.Narrative.UI;
using Celeste.Tools.Attributes.GUI;
using UnityEngine;

namespace DatingBros.PhoneSimulation.UI
{
    public class TextMessageConversationNarrativeView : NarrativeView
    {
        #region Properties and Fields

        [SerializeField] private RectTransform textContentRoot;
        [SerializeField] private GameObject textPrefab;
        [SerializeField, InlineDataInInspector] private TextMessageUIController.VisualSettings playerMessageVisualSettings;
        [SerializeField, InlineDataInInspector] private TextMessageUIController.VisualSettings otherMessageVisualSettings;
        [SerializeField] private float nextMessageTapTimer;
        [SerializeField] private Celeste.Events.Event nextMessage;

        [NonSerialized] private float currentTapTimer;
        [NonSerialized] private bool tapBegan = false;

        #endregion

        public override bool IsValidForNode(Celeste.FSM.FSMNode fsmNode)
        {
            return fsmNode is IDialogueNode && fsmNode is ICharacterNode;
        }

        public override void OnNodeEnter(Celeste.FSM.FSMNode fsmNode)
        {
            IDialogueNode dialogueNode = fsmNode as IDialogueNode;
            bool isPlayer = dialogueNode.UIPosition == UIPosition.Right;

            GameObject textPrefabInstance = Instantiate(textPrefab, textContentRoot);
            TextMessageUIController textMessageUIController = textPrefabInstance.GetComponent<TextMessageUIController>();
            textMessageUIController.Hookup(dialogueNode.Dialogue, isPlayer ? playerMessageVisualSettings : otherMessageVisualSettings);

            ResetTapTimer();
        }

        public override void OnNodeUpdate(Celeste.FSM.FSMNode fsmNode)
        {
            if (tapBegan)
            {
                currentTapTimer += Time.deltaTime;
            }
        }

        public override void OnNodeExit(Celeste.FSM.FSMNode fsmNode)
        {
            ResetTapTimer();
        }

        private void ResetTapTimer()
        {
            tapBegan = false;
            currentTapTimer = 0;
        }

        private void StartTapTimer()
        {
            tapBegan = true;
            currentTapTimer = 0;
        }

        private void StopTapTimer()
        {
            if (currentTapTimer <= nextMessageTapTimer)
            {
                nextMessage.Invoke();
            }

            ResetTapTimer();
        }
        
        #region Callbacks

        public void OnPointerFirstDown(InputState inputState)
        {
            StartTapTimer();
        }

        public void OnPointerFirstUp(InputState inputState)
        {
            StopTapTimer();
        }
        
        #endregion
    }
}