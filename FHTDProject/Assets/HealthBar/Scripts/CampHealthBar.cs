using UnityEngine;
using System.Collections;
using TMPro;

namespace UIHealthAlchemy
{
    [ExecuteInEditMode]
    public class CampHealthBar : HealthBarLogic
    {
        [SerializeField] protected Material mat;
        protected float _Value;
        [SerializeField]
        protected TextMeshProUGUI txtMaxHp, txtCrtHp;

        public override float Value
        {
            get
            {
                return _Value;
            }

            set
            {
                if (_Value != value)
                {
                    _Value = value;
                    if (_Value > 1)
                        _Value = 1;
                    if (_Value < 0)
                        _Value = 0;
                    mat.SetFloat("_Value", _Value * (Max - Min) + Min);
                    this.value = value;
                }
            }
        }
        [Range(0.0f, 1.0f)]
        [SerializeField]
        protected float value;
        [SerializeField] protected float Min = 0;
        [SerializeField] protected float Max = 1;

        void Start()
        {
            Value = value;
        }

        private void Update()
        {
            Value = value;
        }

        public void UpdateBowl(int _iMaxHp, int _iCrtHp)
        {
            float _fValue = (float)_iCrtHp / _iMaxHp;
            txtMaxHp.text = _iMaxHp.ToString();
            txtCrtHp.text = _iCrtHp.ToString();
            value = _fValue;
        }
    }
}
