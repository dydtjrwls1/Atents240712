namespace _01_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This is Juseok. Hello World!

            /*
             * This is multiple Juseok.
             */

            /// Press Enter.
            
            // 디버깅용 단축키 
            // F9 : 브레이크 포인트 지정
            // F5 : 디버그 모드 시작. 디버깅 중일 때는 다음 브레이크 포인트까지 진행
            // F10 : 현재 멈춰있는 지점에서 다음 점으로 넘어가기
            
            // 편집용 단축키
            // Ctrl + D : 현재 코드를 한줄 복사해서 붙여넣기
            // SHIFT + Del : 현재 줄 삭제하기
            // Ctrl + 좌우화살표 : 단어 단위로 이동하기
            // Ctrl + 위아래화살표 : 커서는 그대로 두고 페이지만 이동하기
      
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Bye! Bye!");


            // 변수 : 데이터를 담아 놓는 곳(메모리에서의 위치)
            // 함수 : 특정한 기능을 수행하는 코드 덩어리
            // 클래스 : 특정한 동작을 하는 물체를 표현하기 위해 변수와 함수를 모아 놓은 것

            // 데이터 타입 : 변수의 종류
            // 정수(Integer) : 소수점이 없는 숫자(0, 10, -30 등등)
            // 실수(Float) : 소수점이 있는 숫자(3.14, 5.003 등등)
            // 불리언(Boolean) : true나 false만 저장하는 데이터 타입
            // 문자열(String) : 글자 여러개를 저장하는 데이터 타입

            int a = 10; // integer 타입으로 변수를 만들고 이름을 a라고 붙이고 a에 10라는 값을 넣어라
            a = 2100000000;
            // a = 400000000000 // 사이즈를 넘어가면 실행한됨
            // a = 21.4f        // 데티어 타입이 다르면 안됨

            float b = 3.14f;
            b = 20; // int의 표현범위가 float 보다 작기 때문에 가능
            bool c = true;
            string d = "Hello";
        }
    }
}
