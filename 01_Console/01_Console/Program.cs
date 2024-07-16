namespace _01_Console
{
    internal class Program
    {
        enum AgeCategory
        {
            Child,
            Elementary,
            Middle,
            High,
            Adult
        }
        enum GradeCategory
        {
            A,
            B,
            C,
            D,
            F
        }

        // 함수 이름 : 다른 함수와 구분하기 위한 것(사람용)
        // 파라메터 : 함수를 실행하기위해 필요한 데이터 (0개 이상)
        // 리턴타입 : 함수가 종료되었을 때 돌려주는 데이터 타입(void 는 리턴값이 없다는 의미)
        // 함수바디 : 함수가 실행될 때의 실제 코드

        static void PrintMyData(string name, string age, string address)
        {
            Console.WriteLine($"이름 : {name} | 나이 : {age} | 주소 : {address}");
        }
        static void Main(string[] args)
        {
            //// 7-12 ---------------------------------------------------------------------------------
            //// This is Juseok. Hello World!

            ///*
            // * This is multiple Juseok.
            // */

            ///// Press Enter.

            //// 디버깅용 단축키 
            //// F9 : 브레이크 포인트 지정
            //// F5 : 디버그 모드 시작. 디버깅 중일 때는 다음 브레이크 포인트까지 진행
            //// F10 : 현재 멈춰있는 지점에서 다음 점으로 넘어가기

            //// 편집용 단축키
            //// Ctrl + D : 현재 코드를 한줄 복사해서 붙여넣기
            //// SHIFT + Del : 현재 줄 삭제하기
            //// Ctrl + 좌우화살표 : 단어 단위로 이동하기
            //// Ctrl + 위아래화살표 : 커서는 그대로 두고 페이지만 이동하기

            //Console.WriteLine("Hello, World!");
            //Console.WriteLine("Bye! Bye!");


            //// 변수 : 데이터를 담아 놓는 곳(메모리에서의 위치)
            //// 함수 : 특정한 기능을 수행하는 코드 덩어리
            //// 클래스 : 특정한 동작을 하는 물체를 표현하기 위해 변수와 함수를 모아 놓은 것

            //// 데이터 타입 : 변수의 종류
            //// 정수(Integer) : 소수점이 없는 숫자(0, 10, -30 등등)
            //// 실수(Float) : 소수점이 있는 숫자(3.14, 5.003 등등)
            //// 불리언(Boolean) : true나 false만 저장하는 데이터 타입
            //// 문자열(String) : 글자 여러개를 저장하는 데이터 타입

            //int a = 10; // integer 타입으로 변수를 만들고 이름을 a라고 붙이고 a에 10라는 값을 넣어라
            //a = 2100000000;
            //// a = 400000000000 // 사이즈를 넘어가면 실행한됨
            //// a = 21.4f        // 데티어 타입이 다르면 안됨

            //float b = 3.14f;
            //b = 20; // int의 표현범위가 float 보다 작기 때문에 가능
            //bool c = true;
            //string d = "Hello"; // 문자열, 문자가 여러개 있다. (직접 비교는 계산량이 너무 많아지기 때문에 최대한 피해야 한다.)

            //// 7-15 ---------------------------------------------------------------------------------
            //Console.WriteLine("저는 용석진입니다. 나이는 28세 입니다.");
            //int age = 28;
            //Console.WriteLine("저는 용석진입니다. 나이는 " + age + "살 입니다."); // 절대 비추천

            //string test1 = "테스트";
            //string test2 = "22222";
            //string test3 = test1 + test2;
            //Console.WriteLine(test3);
            //test3 = "Hello, Hello";
            //Console.WriteLine(test3);

            //Console.WriteLine($"저는 용석진 입니다. 나이는 {age}살 입니다."); // 합칠 때는 이런 방식으로 처리

            //// null : 비어 있다라는 것을 표시하는 단어. 기본적으로 참조타입만 가능
            //// nullable type : 널 가능한 타입. 값타입에 붙여서 사용 가능.

            //int? a; // a는 null
            //a = 10; // a는 10

            //string? result = Console.ReadLine();
            //Console.WriteLine(result);

            //// 값타입(Value type) : 스택 메모리에 저장.
            //// 참조타입(Reference type) : 힙 메모리에 저장.
            ///

            // 실습
            // 1. 이름 입력받기 ( "이름을 입력하세요 : " 라고 출력하고 입력 받기 )
            // 2. 나이 입력받기 ( "나이을 입력하세요 : " 라고 출력하고 입력 받기 )
            // 3. 주소 입력받기 ( "주소를 입력하세요 : " 라고 출력하고 입력 받기 )
            // 4. 이름, 나이, 주소를 한번에 출력하기.



            //Console.Write("이름을 입력하세요 : ");
            //string? name = Console.ReadLine();
            //Console.Write("나이를 입력하세요 : ");
            //string? ageString = Console.ReadLine();
            //Console.Write("주소를 입력하세요 : ");
            //string? address = Console.ReadLine();

            //PrintMyData(name, ageString, address);


            // 제어문(Control Statement)

            //int age = 21;
            //Console.Write("나이를 입력하세요 : ");
            //string? ageString = Console.ReadLine();
            //age = int.Parse(ageString);

            //// if : () 사이에 있는 조건이 참이면 {} 사이에 코드를 실행한다.
            //if(age > 20)
            //{
            //    Console.WriteLine("성인 입니다.");
            //}

            ////if(age <= 20) 비추천, 두번 확인함
            //if (age < 21)
            //{
            //    Console.WriteLine("미성년자 입니다.");
            //}

            //if(age > 20)
            //{
            //    Console.WriteLine("성인 입니다.");
            //} else
            //{
            //    Console.WriteLine("애새끼 입니다.");
            //}

            //실습
            // 1. 나이를 입력받기
            // 2. 8살 미만이면 "미취학 아동입니다." 출력
            // 3. 13 살 미만이면 "초등학생 입니다." 출력
            // 4. 16살 미만이면 "중학생 입니다." 출력
            // 5. 19살 미만이면 "고등학생 입니다." 출력

            //Console.Write("나이를 입력하세요 : ");
            //int age = int.Parse(Console.ReadLine());
            //int category = 0; // 카테고리를 숫자로 지정 하는 매우 안좋은 습관 (매직넘버)
            //AgeCategory ageCategory;

            //if (age < 8)
            //{
            //    Console.WriteLine("미취학 아동입니다.");
            //    ageCategory = AgeCategory.Child;
            //} else if (age < 13)
            //{
            //    Console.WriteLine("초등학생 입니다.");
            //    ageCategory = AgeCategory.Elementary;
            //} else if (age < 16)
            //{
            //    Console.WriteLine("중학생 입니다.");
            //    ageCategory = AgeCategory.Middle;
            //} else if (age < 19)
            //{
            //    Console.WriteLine("고등학생 입니다.");
            //    ageCategory = AgeCategory.High;
            //} else
            //{
            //    Console.WriteLine("성인 입니다.");
            //    ageCategory = AgeCategory.Adult;
            //}

            //// switch : () 사이에 있는 값에 따라 다른 코드를 수행하는 조건문
            ////switch(category)
            ////{
            ////    case 0:
            ////        Console.WriteLine("미취학 아동은 1000원 입니다.");
            ////        break;
            ////    case 1:
            ////        Console.WriteLine("초등학생은 2000원 입니다.");
            ////        break;
            ////    case 2:
            ////        Console.WriteLine("중학생은 3000원 입니다.");
            ////        break;
            ////    case 3:
            ////        Console.WriteLine("고등학생은 4000원 입니다.");
            ////        break;
            ////    case 4:
            ////        Console.WriteLine("성인은 5000원 입니다.");
            ////        break;
            ////}


            //switch (ageCategory)
            //{
            //    case AgeCategory.Child:
            //        Console.WriteLine("미취학 아동은 1000원 입니다.");
            //        break;
            //    case AgeCategory.Elementary:
            //        Console.WriteLine("초등학생은 2000원 입니다.");
            //        break;
            //    case AgeCategory.Middle:
            //        Console.WriteLine("중학생은 3000원 입니다.");
            //        break;
            //    case AgeCategory.High:
            //        Console.WriteLine("고등학생은 4000원 입니다.");
            //        break;
            //    case AgeCategory.Adult:
            //        Console.WriteLine("성인은 5000원 입니다.");
            //        break;
            //}

            //int point;
            //if (int.TryParse(Console.ReadLine(), out point))
            //{
            //    Console.WriteLine("정상 변환.");
            //} else
            //{
            //    Console.WriteLine("변환 실패");
            //}

            // 실습 
            // 1. 성적용 enum 만들기 (A, B, C, D, F)
            // 2. 점수를 입력 받아서 90점 이상이면 A, 80이상 B, 70이상 C, 60이상 D, 나머지 F를 출력

            //Console.Write("성적을 입력하세요 : ");
            //GradeCategory category = GradeCategory.F;
            //if (int.TryParse(Console.ReadLine(), out int score))
            //{
            //    if (score > 89)
            //    {
            //        category = GradeCategory.A;
            //    } else if (score > 79)
            //    {
            //        category = GradeCategory.B;
            //    } else if (score > 69)
            //    {
            //        category = GradeCategory.C;
            //    } else if (score > 59)
            //    {
            //        category = GradeCategory.D;
            //    }

            //    Console.WriteLine($"당신의 점수는 {score} 등급은 {category} 입니다.");
            //}
            //else { Console.WriteLine("error."); }

            //int a = 123;
            //a = 0b_0111_1011; // 2진수로 쓴 123

            //int b1 = 0b_1010;
            //int b2 = 0b_1100;

            //int c1 = b1 & b2; // 0b_1000.
            //int c2 = b1 | b2; // 0b_1110.


            // 7/16 ----------------------------------------------------------------------------------

            //int count = 0;
            //while(count < 5)
            //{
            //    Console.WriteLine("Hello");
            //    count++;
            //}

            //count = 0;
            //do
            //{
            //    Console.WriteLine("Hello");
            //    count++;
            //} while(count < 3);

            //for(int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine("Hello");
            //}

            //int[] intArray;
            //intArray = new int[3];
            //intArray[0] = 1;
            //intArray[1] = 2;
            //intArray[2] = 3;

            //foreach( int i in intArray )
            //{
            //    Console.WriteLine($"hello - {i}");
            //}

            // 실습 . 구구단 출력하기

            Console.Write("숫자를 입력하세요 : ");

            int input;
            while(!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Write("다시 입력하세요 : ");
            }

            PrintMulty(input);
            Console.WriteLine("==========================");

            
        }
        
        static void PrintMulty(int input)
        {
            for(int i = 1; i < 10; i++)
            {
                Console.WriteLine($"{input} * {i} = {input * i}");
            }
        }
    }
}
