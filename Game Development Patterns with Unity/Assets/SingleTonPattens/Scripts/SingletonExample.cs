using UnityEngine;


namespace Chapter.Singleton
{
    public class SingletonExample : SingleTon<SingletonExample>
    {
        // �� Ŭ������ �̱��� ������ ����ϹǷ� �ϳ��� �ν��Ͻ��� �����մϴ�.

        // ���⿡ �̱��� Ư�� �Ӽ� �� �޼��带 �߰��ϼ���.
        private static int _score; // ������ ������ �����ϴ� �����Դϴ�.

        public static int Score { get { return _score; } private set { _score = value; } } // ������ ������ �а� �� �� �ִ� �Ӽ��Դϴ�.

        public void IncreaseScore(int points)
        {
            Score += points; // ������ ������Ű�� �޼����Դϴ�.
        }
    }
}