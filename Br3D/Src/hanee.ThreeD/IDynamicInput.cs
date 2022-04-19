namespace hanee.ThreeD
{
    interface IDynamicInput
    {
        // 현재 상황에 맞게 control을 업데이트 한다.
        void UpdateControls();

        // dynamic input 초기 상황으로 초기화
        void Init(devDept.Eyeshot.Environment environment);
    }
}
