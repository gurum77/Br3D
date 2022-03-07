using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Labels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace hanee.Geometry
{
    // 객체의 기본 클래스 ID를 관리하기 위함
    // 추후 엔진단으로 옮길 예정
    // ID를 가지는 모든 객체(Hg3D 에서 Entity base 수준임)
    [Serializable]
    public class Element : ISerializableHelper
    {
        public Element()
        {
            id = new ElementID();
            name = "";
        }

        virtual public void Serialize(SerializationInfo info, bool serialize)
        {
            id = SerializeHelper.Serialize<ElementID>(info, "id", id, serialize);
            name = SerializeHelper.Serialize<string>(info, "name", name, serialize);
        }

        public Element(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, false);
        }

        virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, true);
        }

        public ElementID id { get; set; }
        public string name { get; set; }
        public bool doingTransation { get; set; }
        public event EventHandler AfterEndTransaction;

        public void ClearEventHanders(string eventName)
        {
            if (eventName == "AfterEndTransaction")
                ClearAfterEndTransaction();
        }

        public void ClearAfterEndTransaction()
        {
            AfterEndTransaction = null;
        }
        // tree node properties를 리턴한다.
        // 필요시 재정의
        virtual public List<string> GetTreeNodeProperties()
        {
            return null;
        }

        // 편집 시작
        virtual public void StartTransation()
        {
            if (doingTransation)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            doingTransation = true;
        }

        // 편집 끝
        virtual public void EndTransation()
        {
            if (!doingTransation)
            {
                System.Diagnostics.Debug.Assert(false);
            }

            doingTransation = false;

            // event handler
            if (AfterEndTransaction != null)
                AfterEndTransaction(this, EventArgs.Empty);
        }

        // 파일 열기를 하고 나서 동기화를 해야할 필요가 있을때 재정의 한다.
        // XSectionPlan에서 재정의 함
        virtual public void SyncAfterDeserialize()
        {
        }

        // element를 entities 에 부착한다.
        public void Attach(EntityList entities)
        {
            foreach (var ent in entities)
            {
                Attach(ent);
            }
        }

        public void Attach(Label lab)
        {
            lab.LabelData = this;
        }

        public void Attach(Entity ent)
        {
            ent.EntityData = this;
        }
    }


}
