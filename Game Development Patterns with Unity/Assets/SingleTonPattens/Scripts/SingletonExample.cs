using UnityEngine;


namespace Chapter.Singleton
{
    public class SingletonExample : SingleTon<SingletonExample>
    {
        // 이 클래스는 싱글톤 패턴을 사용하므로 하나의 인스턴스만 존재합니다.

        // 여기에 싱글톤 특정 속성 및 메서드를 추가하세요.
        private static int _score; // 게임의 점수를 저장하는 변수입니다.

        public static int Score { get { return _score; } private set { _score = value; } } // 게임의 점수를 읽고 쓸 수 있는 속성입니다.

        public void IncreaseScore(int points)
        {
            Score += points; // 점수를 증가시키는 메서드입니다.
        }
    }
}