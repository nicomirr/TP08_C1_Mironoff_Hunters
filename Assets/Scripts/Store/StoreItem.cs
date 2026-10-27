using UnityEngine;
using UnityEngine.InputSystem;
using Game.Core;
using System;

//Quizas podria guardarse un currentPlayer, o en lugar de hacer IInteractable hacer otro tipo Como IBuyable, 
//y manejar colision con evento. O tal vez ver como sacar con colision el playerType (interfaz?)

namespace Game.Store
{
    public class StoreItem : MonoBehaviour, IInteractable
    {
        //falta el data de cada item!!!!!!

        //Revisar nombres
        public event Action<PlayerType, int, string> OnPlayerNear;
        public event Action<PlayerType> OnPlayerLeft;

        private bool _canInteract;
        public bool CanInteract => _canInteract;
        public bool HasInteractorInRange { get; set; }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //mandar event OnPlayerNear

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            //mandar event OnPlayerLeft
        }
    }

}

