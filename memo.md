[영상 참조 링크](https://www.youtube.com/playlist?list=PLO-mt5Iu5TeZA0y889ZMi9wJafthif03i)

### 젤리 키우기 게임 - 도트 장면 만들기 [v07+Asset]

#### 장면 만들기

1. 배경 sprite를 하이라키에 배치 room
   1. 위치 초기화
   2. order in layer -2
2. 하늘 배경 추가
   1. 위치 초기화
   2. 하늘 order in layer -4
3. 구름 A,B 추가
   1. 위치 수동조정
   2. order in layer -3
4. shin 이라는 이름의 스프라이트도 추가
   1. 위치 초기화
   2. order in layer 1
   3. color의 알파값을 25정도로
   4.
5. Jelly 0 추가 (Jelly)
   1. 위치 초기화
   2. 자식으로 shadow추가
   3. order in layer -1
   4. animations 폴더내에 ac3 를 추가
6. particles 폴더내에 shin dust를 하이라키에 추가
   1. inspector내에서 shape탭을 전개하면
   2. 파티클의 범위를 볼 수 잇음 (Scene 탭에서 보이게 됨)
   3. 위치 -2.9,1,-1
   4. 복사본 y축 대칭으로 하나 더
7. 테스트
   1. 도트가 조금 망가진것을 확인

#### 픽셀 카메라

1. 카메라 객체 내에 pixel perfect 컴포넌트 추가
2. 속성값 조정
   1. assets pixels per Unit 16
   2. reference Resolution
      1. x 200 y 95
   3. run in edit mode를 클릭해줘야한다.
3. game 탭에서 19:9 로 해주고 조정

#### ~~볼트설치~~

1. visual scripting 사용

#### 볼트 손풀기

1. MyScrolling
   1. ~~스스로 해보기~~
   2. ![[Pasted image 20240729221101.png]]
2. Scrolling
   1. transform translate사용
   2. update내에서 사용되기에 delta time 필요
   3. ![[Pasted image 20240729222558.png]]
3. 원위치 계획
   1. get position과 get x로 위치 받아오기
   2. ![[Pasted image 20240729230123.png]]

젤리 키우기 게임 - 알아서 돌아다니는 젤리 A.I 구현하기 [유니티 볼트 기초 강좌 V08]
젤리가 직접 돌아다니는 ai 제작 계획

시작전
최신버전으로 올라가면서 Trigger Unity Event를 바로 생성할 수 없는 문제가 생겼습니다. Edit > Project Settings > Visual Scripting > Type Option 에서 +버튼 누르시고 State Machine 추가한 후 아래의 Regenerate Nodes 한번 눌러주시면 노드 만드실 수 있습니다.

#### 스테이트 머신

1. Jelly State machine을 만들어준다.
2. 그내에서 script state를 추가해준다.(노드 추가)
   1. Walk
3. Start 노드를
   1. Idle
   2. Only Standing
   3. 으로 변경해준다.
   4. 노드 내부로 이동
4. 처음에 로딩이 되면 일정시간 대기후 이동하기를 바라기에
   1. 타이머, 랜덤, trigger unity Event node가 필요
   2. ![[Pasted image 20240729232748.png]]
5. Idle에서 walk로 make transition 해준다.
6. no event에서 이벤트를 추가해준다.
   1. ![[Pasted image 20240729232719.png]]
7. Walk 노드 내에서
   1. ![[Pasted image 20240729233019.png]]
8. edit > preference
   1. visual scripting
   2. transitions Reveal
      1. ![[Pasted image 20240729233148.png]]
      2. 필요시 원하는 값으로 수정

#### 이동하기

1. Walk state로 진입했을때
   1. (On Enter State)
      1. set bool 노드 추가
      2. ![[Pasted image 20240730070612.png]]
   2. On Exit State에서는
      1. set bool false
      2. ![[Pasted image 20240730070619.png]]
2. On Update
   1. Object 급으로 SpeedX와 SpeedY(float)를 만들어준다.
      1. get으로 변수를 가져와주고
      2. delta time 유닛도 챙겨준다. multiply 유닛 필요
      3. transform translate를 사용
      4. ![[Pasted image 20240730072917.png]]
      5. y축의 값에 따라 z에도 영향을 주도록 설계 (원근감)
   2. 현재 SpeedX와 Y에 대한 값의 초기화를 주지 않아서 0인상태
   3. UnityEvent가 발생해서 transition중일때 초기화를 할예정
   4. 슈퍼유닛을 만들어서 조립할예정
3. Macros내에 SuperUnit이라는 폴더만들어주고 script machine 추가(SetWalk)
4. 슈퍼 유닛을 만드는 법
   1. input output이 필요
   2. input
      1. input(nesting) output(nesting) 추가하고 시작
         1. ![[Pasted image 20240730073755.png]]
      2. input을 설정하고 graph inspector에서 trigger input과 data input을 넣어준다.
         1. trigger input
            1. in / hide level check
         2. data inputs
            1. x / type float
            2. y / type float
   3. output
      1. ![[Pasted image 20240730075000.png]]
5. Set Variable을 만들고
6. SetSpeed 전체
   1. ![[Pasted image 20240730075134.png]]
7. SetSpeed를 외부에 연결
   1. ![[Pasted image 20240730080517.png]]
   2. 다음과 같이 연결을하면
   3. X와 Y에 별개의 랜덤값이 부여됨
8. 테스트
   1. 정상

#### 경계설정

1. 빈 객체 추가 (BackGround Group)
   1. 위치 초기화
   2. VisualScripting SceneVariables와 카메라와 Jelly를 제외한 배경관련 객체들을 넣어준다.
2. 빈 객체 추가 (Border Group) 4. Border Group 내에 빈객체 추가 (Top Left) 1. Move Tool 을 사용하여 경계를 맞춰줌 또는 2. x -5 y 1 5. Top left 복사(Bottm Right) 1. x 5 y -2
3. Jelly의 Object 단위의 변수에 TopLeft 와 BottomRight를 추가해주고
   1. 자료형은 transform
   2. 객체들을 끌어와서 연결해준다.
4. 새로운 슈퍼 유닛 생성(CheckBorder)
   1. input,output을 만들어준다. (nesting)
   2. transform get position
   3. Object의 변수를 가져오고 그 값을
      1. get position에 연결해주고 그값을
         1. vector3 get x 또는 y를 해주어서 값들을 뽑아내야한다.
   4. 논리적으로 연산후 모아줌
      1. ![[Pasted image 20240730122415.png]]
      2. 연결 논리적으로 검산하기 !
   5. output에서는
      1. 2개의 output을 들고 이번엔 라벨을 가리지 않는다.
5. Jelly의 Walk State 내부로 이동한다.
   1. onupdate 의 끝에 translate 후에 CheckBorder 슈퍼유닛을 넣어서 상황을 체크한다.
   2. ![[Pasted image 20240730122625.png]]
6. 테스트
   1. 정상
      1. 밖의 경계일때 로그가 정상출력

#### 돌아가기

1. 캐릭터가 외곽으로 나가면
   1. 태양빛을 받는 중앙으로 돌아오게 하려는 계획
   2. 이 위치는 GameManager가 관리할 계획
2. GameManger와 스크립트 생성 (.cs까지)
   1. 위치 초기화 넣어주기
   2. 코드
      ```cs
      public class GameManager : MonoBehaviour
      {
      	public Vector3[] pointList;
      }
      ```
   3. Scene 급 변수목록에
      1. Manager라는 변수추가
      2. 자료형 Game Object 드래그로 GameManager 추가해주기
3. 바로 사용 가능한것은 아니다
   1. file > project >Settings > visual scripting 에서
   2. Regenerate Nodes를 눌러줘야한다.
   3. 추후 이런 자동완성이 가능해짐
   4. ![[Pasted image 20240730130246.png]]
4. 슈퍼유닛 생성 (SetPoint)(script machine)
   1. input output 생성
   2. 씬에 잇는 Manager 변수를 가져오고
   3. 그내부에 있는 pointlist 변수를 가져오고
   4. get list item 유닛으로 배열 데이터 가져오기
   5. 인덱스를 랜덤으로 주기위해 Random(int) 유닛 을 가져온다.
   6. ![[Pasted image 20240730131010.png]]
5. Jelly Walk state로 돌아가서
6. outside 일경우 Turn이라는 유니티 이벤트를 발생시키기
7. ![[Pasted image 20240730131235.png]]
8. 그다음 Walk를 우클릭하여 Make Selt transition을 추가해준다.
9. 목표지점과 현재지점의 차이를 계산하여 방향(벡터)를 구한후
   1. 속도를 적절히 부여 한 후
   2. Set Walk로 속도값을 넘겨준다.
10. 테스트

    1. jelly를 여러개 만들고 실행해도 잘되는것을 확인
    2. 정상

### 젤리 키우기 게임 - 인터페이스 만들기 [V09]

#### 캔버스 설정

1. 캔버스를 만들고 더블클릭하면 크게나오는데 이유는 2. Render Mode가 Overlay 이기 때문이다. 3. mode를 카메라로 바꾼뒤 4. main camera를 드래그 드랍으로 넣어준다.
2. Pixel Perfect 체크
3. 사이즈가 안맞는다면
   1. camera 내의 size가 3인지 확인
4. Plane Distance
   1. 카메라와의 z축 거리
   2. (0을 주면 카메라와같이 z값이 -10인상태)
   3. 우리는 값을 5를 준다.
5. Order in layer 5
6. Canvas내에 Canvas Scaler 속성
   1. _이 값도 모바일 빌드까지 고려햇을때 매우 중요한 속성_
   2. Scale with ScreenSize
   3. 해상도 x 190,y 90
   4. Reference Pixels Per Unit 16

#### 버튼 만들기

1. Canvas 밑에 버튼을 담을 Image 추가(Left Btn)
   1. 이미지 소스 panel
   2. 가로 32 세로 12
   3. 앵커 좌하단에 배치
   4. pos x 1 pos y 1
   5.
2. 그(Left Btn) 밑에 실제 버튼 추가 (Jelly Button)
   1. 이미지 소스 Icon 2
   2. set native size
   3. 하위의 text 제거
   4. 버튼 컴포넌트내에 Transition 속성을 Color Tint > Sprite Swap으로 변경
   5. Navigation none으로 변경
   6. 위치 pos x -7.5 pos y 5
3. Jelly Button 을 복사(Plant Button) 8. 위치 pos x 7.5 9. icon3로 이미지 소스 변경
4. Left Btn 객체내에 이미지 추가(Icon Shadow)
   1. 이미지 소스 Icon Shadow
   2. 첫번째 자식이 되도록 순서변경
   3. pos x -7.5
5. Icon Shadow를 복사
   1. pos x 7.5
6. 테스트
   1. 정상
7. Left Btn버튼 복사 (Right Btn)
   1. 앵커 우하단
   2. pos x -1 pos y 1 width 20
   3. plant 부분 삭제
   4. 나머지 두개 pos x 0
   5. Jelly Button의 이미지 소스를 icon 5로 교체(Sell Button)
      1. transition 내부 값들도 icon 5 over로 교체

#### 텍스트 만들기

1. Canvas 내에 이미지 추가(Left Text)
   1. width 35 height 8
   2. 앵커 좌상단
   3. pos 2 pos y -3
   4. 이미지소스 패널
   5. color의 알파값 150
   6. Left Text내에 image(Border)
      1. 앵커 전체확장
      2. 이미지 소스 panel border
   7. Left Text내에 새로운 image(Icon Jellatin)
      1. 이미지 소스 icon 0
      2. set native size
      3. pos x -1 pos y 1
   8. Left Text내에 새로운 Text(Jelatin Text)
      1. 폭, 높이 / 0,0
      2. pos x -2
      3. overflow,overflow
      4. 우측정렬,중앙정렬
      5. 앵커 우측에 붙이기
      6. 폰트 도현체
      7. 라벨 999,999,999
   9. Jelatin Text 복사
      1. 우상단에 배치
      2. pos x -1 pos y -3
      3. 이하 이미지 소스 icon 1 (Gold Icon)
      4. Gold Text

#### 재화 시스템

1. 재화 시스템 변수는 Saved급으로 만들기
2. Jelatine , Gold (Integer)
3. Jelatin Text 내에 script machine 추가 (임베드)
   1. Object 단위로 변수 추가 (Value / Integer)
   2. Jelatine을 가져와서 Value에 값으로 연결
   3. ![[Pasted image 20240730190611.png]]
   4. Set Variable 노드의 입력의
      1. 두번째에 넣지 않도록 주의 (네번째에 넣어야함)
4. 계산 완료된 값을 사용하는 UI는 LateUpdate에서 처리
5. Events > Lifecycle > lateupdate
6. math에 보면 _Smooth Step_ 이라는 유닛이 있음
   1. From 값을 To 까지 T의 속도로 부드럽게 변경
   2. T의 값에 대해서
      1. 0은 불변
      2. 1은 즉각변화(애니메이션 x)
      3. 현재는 0.5 사용
7. format 유닛을 챙겨준다.
   1. {0:n0}
   2. 알아서 쉼표를찍어줌
8. ![[Pasted image 20240730191635.png]]
9. 오브젝트 변수(Value)가 Save변수를 따라가기 위해서 프레임마다 저장
10. 전체 복사해준후 Gold Text에도 동일하게 적용
    1. Object 변수 만드는것 잊지않기 (Value / Int)
    2. 이번에는 참조하는 Saved값이 Jelatine이 아닌 Gold임에 맞춰서 값 변경
11. 테스트 준비
    1. Saved 변수의 초기값이 100 / 200이 되도록 설정후 저장 후 실행
    2. SmoothStep이나 SmoothDamp로 숫자 애니메이션 구현

### 젤리 키우기 게임 - 👆클리커 기능 구현 [V10]

#### 클리커 시스템

1. Jelly state graph 내에서 새로운 state script 생성
   1. 타이틀 Touch
   2. 설명 Clicker system
2. 터치 시스템을 위해서 Jelly 내에 Collider 를 추가해준다.
   1. circle collider 2d (is trigger 체크)
   2. 물리효과를 줄것은 아니기에 rigidbody 2d는 필요 없음
3. Idle > Touch 와 Walk > Touch에 해당하는 Transition을 만들어준다.
4. 이동하는 이벤트로 이동후
   1. Event > Input > OnMouseDown 유닛 추가
      1. 콜라이더를 가지고잇는 객체를 눌렀다
   2. ![[Pasted image 20240730193416.png]]
   3. 모든 Touch 로 향하는 transition에 동일하게 적용해준다.
   4. Touch에서도 self transition을 만들어주고 동일하게 구현
   5. ![[Pasted image 20240730193558.png]]
5. Touch state 내부구현
   1. animator 내에서 Touch state가 있고 그쪽을 활성화 하기위해서는 doTouch Trigger가 필요하다.
   2. 그리고 원하는 행위후
   3. state 돌려놓기 위한 트리거 발생
   4. ![[Pasted image 20240730214932.png]]
6. 다시 Touch > Walk transition를 생성해주고
   1. 내부에 UnityEvent Walk를 연결해준다.
   2. ![[Pasted image 20240730215739.png]]
7. 테스트
   1. 정상

#### 재화 증가

1. 별개의 매크로(슈퍼유닛) 제작 예정
   1. script machine 생성(GetJelatin)
   2. input output 추가
   3. 진행 전 확인 사항
      1. 젤리 수 확인 sprites > ingame폴더 내부를 보면 jelly가 12종이 준비되어있음 (0~11)
      2. Jelly의 객체이름이 정확히 Jelly여야함
   4. Jelly 내에 Object 변수를추가해준다 .(ID / Int)
   5. Jelly 내에 Object 변수를추가해준다 .(Level / Int / 1)
2. 다시 슈퍼 유닛인 GetJelatin 에서 작업해준다. 2. 젤라틴을 증가시키는 로직 1. ![[Pasted image 20240730222318.png]] 3. 저장하는 로직 1. ![[Pasted image 20240730222329.png]]
3. Jelly 의 Touch state 내에서 위로직을 연결해준다.
   1. 로그자리에 연결
      1. ![[Pasted image 20240730222543.png]]
4. 테스트
   1. 테스트 진행 중 젤리를 복사해서
      1. Jelly sprite 3
      2. ID 3
      3. 클릭시 4젤라틴이 증가되는지 확인
         1. 정상!
5. 레벨 올라가는 로직이 없는상태

#### 젤리 성장

1. Animations 폴더에보면 Ac1,2,3 이 있는데 이 이유가 level 1,2,3 에 해당하는 것이다. (max level은 3)
2. 기존 Jelly의 animator 컨트롤러는 Ac1로 교체
3. 볼트내에서 animator 컨트롤러를 사용하면 내부적 에러발생
4. 게임메니져에서 함수를 만들어서 참고를 할 예정
5. GameManager.cs내에서

   1. RuntimeAnimatorController\[] 자료형인 변수를 생성
   2. 코드

      ```cs
      public class GameManager : MonoBehaviour
      {
          public Vector3[] pointList;

          public RuntimeAnimatorController[] LevelAc;

          public void ChangeAc(Animator anim,int level){
              anim.runtimeAnimatorController = LevelAc[level-1];
          }
      }
      ```

   3. 스크립트가 변경되면 Update를해준다.
      1. edit > project settings > visual scripting > regerate nodes
         1. (거의 50초 소모..)
   4. inspector에서 LevelAc 초기화

6. Jelly 객체 내에 객체변수로 Exp 추가 (float)
7. Jelly의 Touch State 내에
   1. getJelatin을 연결한 직후에 로직추가
   2. ![[Pasted image 20240730225336.png]]
   3. 슈퍼 유닛 추가예정
      1. 기능
         1. 레벨업
         2. 자동 exp 증가
8. 슈퍼 유닛 추가(SetExp)
   1. 기능
      1. 현재 최고 레벨인지 여부체크
      2. 경험치 증가
      3. 레벨업 이벤트가 필요한지 체크
      4.
   2. script machine
   3. input output 추가
   4. 레벨 체크부터
   5. equal 유닛 가져와서 inspector 에서 Numeric 체크
   6. 레벨업 요구 경험치는 현재레벨에 비례로 설정
   7. 레벨업 후 올바른 animation 설정을 위해서
      1. GameManager 내에 만들어둔 ChangeAc 사용
   8. 현재 script machine 이 들고있는 animator를 넣어줘야하기 때문에
      1. self 라는 유닛을 사용
      2. 디테일
         1. ![[Pasted image 20240731080028.png]]
         2. 빨간선 처럼 연결하면 A+1이 2번되므로 문제가 발생
   9. SetExp 전체 Flow
      1. ![[Pasted image 20240731080518.png]]
      2. ![[Pasted image 20240731080527.png]]
      3. ![[Pasted image 20240731080537.png]]
      4. ![[Pasted image 20240731080601.png]]
9. 위 슈퍼유닛을 jelly에 적용
   1. 모든 각각의 state마다 update에 SetExp (매크로)를 적용해준다.
   2. walk state경우 translate 직후
10. 테스트
    1. 정상

#### 그래프 정리

1. 그룹 정리 연습
2. Jelly Idle state 내에서
   1. ![[Pasted image 20240731084043.png]]
3. Jelly Walk state 내에서
   1. ![[Pasted image 20240731084808.png]]
   2. ![[Pasted image 20240731084818.png]]
   3. ![[Pasted image 20240731084828.png]]
4. Jelly Touch State 내에서
   1. ![[Pasted image 20240731085534.png]]
   2. ![[Pasted image 20240731085543.png]]
   3. ![[Pasted image 20240731085553.png]]
5. Transition Event 내에서도 정리
   1. Idle > Walk
      1. ![[Pasted image 20240731091932.png]]
   2. Touch > Walk
      1. ![[Pasted image 20240731091958.png]]
   3. Walk > Walk
      1. ![[Pasted image 20240731092013.png]]
      2. ![[Pasted image 20240731092030.png]]
6. 테스트
   1. 정상
7. _원리파악_
   1. 기존의 animation 방식은 완성된 sprite들의 frame 나열이였다면
   2. 이방식은 같은 sprite 하나를 가지고 Scale만을 조절하여 animate를 만들어냈다.
   3. idle1 은 scale이 0.5이고 idle2는 0.75 idle 3은 1의 값을 가진다.
      1. idle 기준
         1. 1초에 x가 p0.05% 증가
         2. y가 p0.05% 감소
         3. 이후 1초에 걸쳐서 원위치
      2. Walk기준
         1. 15프레임에 x가 p0.15% 증가
         2. y가 p0.15% 감소
         3. 이후 15프레임에 원위치
      3. Touch 기준
         1. 2프레임에
            1. x p0.10% 감소
            2. y p0.20% 증가
         2. 10프레임에
            1. x p0.20% 증가 (총 1.1)
            2. y p0.30% 감소 (총 0.9)
         3. 15프레임에 원위치

### 젤리 키우기 게임 - 판매를 위한 젤리 드래그 & 드랍 [V11]

#### 젤리 집어들기

1. 카메라로부터 입력을 받기 위해서
   1. Scene 변수에 Camera 추가작업
   2. 이름 Camera , 타입 Camera , 값 드래그해서 넣어주기
2. _UI에 대한 설명_
   1. 스크린 좌표계
      1. ![[Pasted image 20240731101609.png]]
   2. 마우스는 스크린 좌표계에서 움직임
   3. 월드 좌표계
      1. ![[Pasted image 20240731101650.png]]
   4. 스크린 좌표계인 마우스 위치를 월드 좌표계로 변경 필요
3. 슈퍼 유닛 생성(GetMousePoint)
   1. input output 필요
   2. 목적이 마우스의 좌표를 스크린 월드 좌표계로 바꿔서 출력해야하기때문에
      1. Vector3도 out쪽에 필요
   3. 카메라에 접근하기위해 Scene 변수에서 가져온다.
   4. get mouse pos를 검색
      1. input 쪽에서의 유닛을 선택
   5. Screen to world point 유닛 추가
      1. 매개변수에 eye가 없는 것을 선택
   6. z축의 값도 y축의 값과 같게 재조정후 export
   7. Vector3 set z의 graph inspector에 보면 Chainalbe 이라는 옵션이 있음 이것을 체크하면
      1. 반환형으로 vector 3의 값을 얻을 수 있음
   8. ![[Pasted image 20240731102827.png]]
4. Jelly의 state flow 내에 script state를 새로 추가해줌
   1. 테스트를 위해 현재는 업데이트만 사용
   2. ![[Pasted image 20240731103216.png]]
   3. Pick을 오른클릭하여 toggle start를 해준다.
   4. Idle은 반대로 잠시 꺼준다.
      1. ![[Pasted image 20240731103311.png]]
   5. 토글들을 원위치
      1. ![[Pasted image 20240731103424.png]]
   6. 필요할때만 위처럼 동작하게 구현 계획

#### 드래그 상태

1. jelly touch 내로 들어가서
2. seq를 검색하여 sequence 유닛을추가
   1. 컨트롤 플로우를 2개로 나누는 역할
3. Graph 변수로 PickTime을 추가한다.
   1. 젤리를 누르거나 드래그하는 시간을 저장하는 그래프 변수 추가
   2. 초기값도 float 0으로 초기화
4. 위 PickTime을 부르는 조건은
   1. Event > input > on mouse drag로 (유닛추가)
5. control > select on flow 라는 유닛을 추가
   1. 여러개의 컨트롤 플로우를 받을 수 잇음
6. 참조
   1. ![[Pasted image 20240731104840.png]]
   2. ![[Pasted image 20240731104846.png]]
7. touch > pick 으로가는 트랜지션을 만들고 event 내로 이동
   1. UnityEvent Pick으로연결
8. 테스트
   1. 정상
   2. 젤리를 놓는 로직 필요
      1. 만약에 젤리를 경계 밖에 드랍할경우 예외처리필요

#### 원위치 돌려놓기

1. Jelly Pick state 내에서
   1. OnExitState 때 예외처리 로직
2. 로직
   1. ![[Pasted image 20240731105813.png]]
   2. 다만 나갔을때 발생하는 이벤트인데
   3. 나갔을때를 감지하는 로직 또는 나가게 해주는 로직이 필요
3. Pick > Idle 로 transition을 추가해주고 event로 이동
   1. On Mouse Up 유닛을 추가하여 연결
      1. ![[Pasted image 20240731110045.png]]
4. 테스트
   1. 정상
   2. 이제 젤리를 Sell Box 쪽에 드랍햇을 때를 구현하기

#### 젤리 가격

1. GameManager 내에 정의 해두기
2. `public int[] jellyGoldList;` 를 추가해준후 unity inspector 창에서 정의
3. scripts 가 변경되었으므로 regenrate nodes를해준다.

#### 젤리 판매

1. 판매 (가능) 여부를 알 수 있도록 플로우 매크로(script machine)을 추가 (ButtonSell)
2. 마우스가 올라왔을때를 UnityEvent Enter로 처리 (나갔을 때는 Exit로 처리)
3. Scene 변수로 isSell을 추가해준다. (Boolean false)
4. ![[Pasted image 20240731112340.png]]
5. Right Btn > Sell Button에 Script machine 추가 (ButtonSell)
6. 위 graph는 이벤트가 발생햇을때의 행동이고 Trigger가 필요하다.
7. Sell Button 객체 내에 Event Trigger 컴포넌트를 추가해준다.
   1. UI의 다양한 이벤트를 연결해주는 컴포넌트
   2. add new event type을 눌러주고
   3. Function 에서는 Script.Machine 내의 TriggerUnityEvent를 만들고
   4. 매개변수로 String을 적어준다.
   5. 다음처럼 구성
      1. ![[Pasted image 20240731112846.png]]
8. 가격을 측정하여 판매를 담당하는 슈퍼유닛 생성(GetGold)
   1. input output 생성
   2. 가격을 가져오기위해 manager를 가져와준다.
   3. 가격 list를 가져와주고
   4. list에서 get item을 한다. (get list item)
   5. level을 곱해주고
   6. 기존 gold에 더해서
   7. Minimum 유닛을 통하여 (최대값 제어)
   8. set gold를해준다.
   9. ![[Pasted image 20240731114602.png]]
9. 위 슈퍼유닛은 Pick > Idle 에서 마우스를 손 땟을 때
   1. isSell 이 True인지 확인 (boolean, if)
   2. 판매중이면 getgold후 destroy
   3. 판매중이아니면 다음 State으로 transition
   4. ![[Pasted image 20240731115241.png]]
10. 테스트
    1. 젤리를 싸게 팔아보고
    2. 비싸게도 팔아본다.
    3. 정상

### 젤리 키우기 게임 - UI 창 구축하기 [V12]

#### UI 구축하기

1. 기존의 canvas 내에 Image 추가(Jelly Panel)
   1. 이미지 소스 panel
   2. width 40 height 50
   3. 앵커 6시방향
2. Jelly Panel 내의 Image 추가(Page Left Btn)
   1. 이미지 소스 Panel left
   2. set native size
   3. 앵커 9시
   4. pos x -8
   5. Button 컴포넌트 추가
      1. Color Tint를 sprite Swap으로 변경
      2. Hightlighted
      3. Pressed
      4. Selected
      5. 의 sprite 를 panel left over로 변경
3. Page Left Btn 복사(Page Right Btn)
   1. 대칭에 맞게 만들어주기
   2. 이미지 right
   3. 앵커
   4. pos x 8
   5. Sprite Swap 내부 속성값 변경
   6. 만약
      1. Target Graphic이 Page Right Btn으로 되어있는지 잘 확인한다.
4. Jelly Panel 복사 (Plant Panel)
   1. 자식 객체들 삭제
   2. width 90
5. Plant Panel를 한번 더 복사(Option Panel)
   1. 소스이미지 none으로 변경
   2. color black 알파값 180
6. Option Panel 내부에 Image 추가()
   1. 가로 80 세로 40
   2. 이미지 소스 panel
   3. 비활성화를 default로

#### 창 애니메이션

1. Jelly Panel과 Plant Panel은 y값을 -55로 내려줌
2. Animation 내에 새로운 폴더(UI)
   1. 애니메이터 (AcUI) 와
   2. 2개의 애니메이션을 만들어줌(Show,Hide)
3. AcUi 애니메이터 설정
   1. AcUI 내에서 Empty state를 추가해주고 (Default state)
   2. 2개의 animation을 넣어준다.
   3. any state > show , any state > hide를 연결해준다.
   4. 파라미터로 doShow와 doHide를 만들어준다.(trigger)
   5. 각 transition 마다
      1. transition 내에 condition 추가
      2. transition duration 은 0 , Has Exit Time 도 uncheck
4. 위 AcUI를 JellyPanel과 Plant Panel에 넣어줌
5. Jelly Panel 객체를 누르고 animation 탭을 확인한다.
6. Show state일때
   1. add property > Rect Transform > Anchored Position +
   2. 0프레임일때 y값이 55가 되게 해준다.
   3. 10프레임일때 y값이 5가 되게 해준다.
   4. 15프레임일때 y값이 2가 되게 해준다.
   5. 59프레임일때도 y값이 2가되게 해준다.
   6. 확인
7. HIde state 일때
   1. add property > Rect Transform > Anchored Position +
   2. 0프레임일때 y값이 2가 되게 해준다.
   3. 10프레임일때 y값이 5가 되게 해준다.
   4. 59프레임일때도 위치를 30프레임 자리로 드래그해준다.
   5. 59프레임일때 y값이 -55가 되게해준다.
   6. 확인

#### 창 열고 닫기

1. Macros 폴더내에 새로운 script machine 생성 (슈퍼유닛은 아님) (ButtonPanel)3.
2. Left Btn > Plant Button과 Jelly Button에 script machine 컴포넌트를 추가
   1. 여기에 ButtonPanel을 연결
3. 버튼과 연결된 상태를 알아야 하고, 버튼과 연결된 판넬도 알아야함
4. Plant Button 내에 Object 급으로 변수를 추가
   1. isClick(Boolean, false),
   2. ShowSprite(Sprite, Icon 3 Over),
   3. HideSprite(Sprite, Icon 3),
   4. Panel(Game Object,Plant Panel)
5. 현재 variables 라는 컴포넌트를 복사후 JellyButton에 붙여넣기(Paste component as new) 후 일부 값 수정
   1. isClick(Boolean, false), (동일)
   2. ShowSprite(Sprite, Icon 2 Over),
   3. HideSprite(Sprite, Icon 2),
   4. Panel(Game Object,Jelly Panel)
6. Left Btn 내에 빈객체 추가(Option Button)
   1. Script machine 추가(새로운 매크로 생성)(ButtonOption)(슈퍼유닛아님)
7. ButtonPanel부터 구성
   1. *On Button Click*이라는 유닛을 추가
   2. 버튼클릭시 어떤 boolean값을 기준으로 toggle처럼 작동하게 만들예정
   3. 상황에 따라서 맞는
      1. 트리거를 작동시키고
      2. Panel 조절
      3. Sprite 변경
      4. boolean값 토글
   4. 을 해야한다.
   5. esc를 눌렀을 때 처리 추가
      1. On Button Input으로 감지 (유닛추가)
      2. 위의 경우 Hide 쪽으로 연결
   6. ![[Pasted image 20240731192914.png]]
8. 위 모든것을 복사한후에 ButtonOption에서 붙여넣기
   1. set trigger(animator)를 replace 하여 set active(game Object)로 변경
   2. get variable Object Sprite 쪽은 삭제를해주고
   3. set variable Object Sprite는 replace 하여 set Time Scale로 바꿔준다.
   4. On Button Click 을 지워주고 (Button UI가 비존재)
   5. Esc눌렀을때 열여야하므로 On Button 자리를 On Button Click(Cancel)로 대체
9. 테스트
   1. 시작후 Left Btn > Jelly Button을 눌러 미리 Script Graph를 열어준후
   2. Game tab내에서 해당버튼을 클릭
   3. Jelly Button과 Plant button은정상작동하지만
   4. OptionPanel을 열때 다른 Panel이 같이 열리는 문제가 있는 상황
   5. Jelly Button을 누른후 Plant Button을 누르면 Panel이 겹치는 문제도 있는 상황
   6. UI를 열은 후에도 Jelly가 클릭되는 상황

#### 로직 보완

1. 이 게임에 일시 정지를 알리는 변수를 하나 추가
   1. Scene급 변수 isLive boolean true
2. Plant Button에 달린 script machine 내로 이동해서
   1. Change State 이후에 Scene에 있는 isLive 값을 가져와서
   2. Panel이 열렸는지 닫혔는지에 따라 알맞은 isLive의 값을 채워준다.
   3. ![[Pasted image 20240731201449.png]]
3. Set isLive 에 대한 유닛들만 복사해서 ButtonOption graph내에서도 동일하게 채워준다.
   1. ![[Pasted image 20240731201504.png]]
4. Jelly가 가지고 있는 state machine 으로 이동
5. ??? > Touch로가는 3가지 transition이 있는데 각각마다 조건을 추가할 예정
6. Idle > Touch부터
   1. ![[Pasted image 20240731201845.png]]
   2. Walk > Touch도 동일
   3. Touch > Touch도 동일
7. esc를 누를때 OptionPanel을 제외한 다른 Panel이 같이 보이는 문제 해결
   1. ButtonPanel에서
   2. ![[Pasted image 20240731202343.png]]
8. 기존 Panel 이 열려 있으면 닫고나서 Panel을 열기위하여
   1. Jelly Button으로가서 Object 단위 변수로 AnotherBtn을 추가
      1. (Game Object) , Plant Button 으로 채우기)
   2. Plant Button은 반대
      1. Object 단위 변수로 AnotherBtn을 추가
      2. (Game Object) , Jelly Button 으로 채우기)
   3. 열렸는지 값(isLive)을 가져와서 확인
      1. 열렸다면 다른 버튼을 가져와서 Hide trigger를보낸다.
      2. 한쪽에서는 Hide trigger를 받아서 처리해준다.
   4. ButtonPanel
      1. ![[Pasted image 20240731203152.png]]
9. ButtonOption graph로 넘어와서
   1. ![[Pasted image 20240731203454.png]]
   2. 다음처럼 변경
      1. isLive가 true일때는 동일
      2. false 일때는 연결없음
10. 테스트
    1. 정상

### 젤리 키우기 게임 - 🔓해금 시스템 만들기 [V13]

#### 젤리 데이터

1. GameManager.cs 에 ~를 추가

```cs
public Sprite[] jellySpriteList;
public string[] jellyNameList; // ID 에 해당하는 고유이름
public int[] jellyJelatinList; // 언락을 위한 젤라틴요구량
```

1. Unity inspector로 와서
   1. 위의 3가지 리스트 값 채워주기
   2. regenerate nodes?

#### UI 페이지

1. Plant Panel은 잠시 비활성화
1. Jelly Panel 내에 Image 추가(Icon)
   1. set native size
   2. pos y 12
1. Jelly Panel 내에 Text 추가(Name Text)
   1. 가로 세로 0 0
   2. Overflow overflow
   3. 중앙 정렬, 중앙 정렬
   4. font size 7
   5. 도현체 폰트
   6. 라벨 슬라임
   7. pos y -3
1. Name Text 복사(Sub Name Text)
   1. pos y -9
   2. 라벨 젤리 // (고정)
   3. font size 6
   4. color 949a9f
1. Jelly Panel 내에 버튼 추가(Buy Button)
   1. 앵커 아래쪽에 가득채우기
   2. 높이 10
   3. left 2 right 2 pos y 3
   4. 소스 이미지 판넬
1. Buy Button내의 text
   1. 앵커 우상단
   2. pos x -2 pos y -2
   3. 높이와 너비는 0 0
   4. Overflow Overflow
   5. 우측 정렬, 상단 정렬
   6. 도현체
   7. 라벨 999,999
1. Buy Button내의 이미지 추가
   1. 이미지 소스 icon 1
   2. set native size
   3. 앵커 좌상단
   4. pos x 가 2 pos y 가 -1.5
1. 완성된 Jelly Panel은 구매가 가능한 상태
1. Jelly Panel내에 빈객체 추가 (Unlock Group)
   1. 앵커 전체(최대)크기
   2. 이 객체 내부로 Icon,Name Text, Sub Name Text, Buy Button을 넣어준다.
   3. 위치를 Jelly Panel 이하의 첫번째 자식위치로 옴겨주고
1. Unlock Group을 복사 (Lock Group)
1. UnlockGroup은 잠시 비활성화
1. LockGroup
   1. 컴포넌트추가 Image(객체 추가가 아닌 컴포넌트 추가)
   2. 이미지 소스 Panel
   3. Color 7b7d80
1. LockGroup내의
   1. Icon의 Image 컴포넌트 내의 Color black
   2. Text Name과 Sub Text Name 삭제
   3. Icon 복사(Lock Image)
1. Lock Image
   1. 이미지 소스 Lock
   2. set native size
   3. pos y -3
   4. color 616366
1. 해금에 필요한 조건은 골드가 아닌 젤라틴
1. Buy Button -> (Unlock Button)내의
   1. Image를 icon 0으로 변경
1. 해금이 안된 상태와 된 상태를 구분할 수 잇도록 UI 구축
1. UnlockGroup을 항상 켜두고 Lock Group을 활성화 비활성화로 제어 +
   1. UnlockGroup이 Lock Group보다 (하이라키상에서) 위에 있어야함
1. Jelly Panel 내에서 빈객체 추가(Lock Group)
   1. 2번째상에 , Unlock 바로 아래에 위치 , 기존 Lock group 보다 위쪽에 위치
   2. 앵커최대
   3. 방금 만든 Lock Group내로 기존 Lock Group을 넣어준다.
1. Jelly Panel에서 Text 추가(Page Text)
   1. 앵커 우상단
   2. pos x -2, y -2
   3. 너비 높이 0 0
   4. 라벨 #01
   5. 폰트 도현체
   6. 우측정렬, 상단정렬
   7. overflow,overflow
   8. 폰트크기 5
1. 현재 배치
   1. ![[Pasted image 20240801090733.png]]

#### 페이지 이동

1. Jelly Panel에 쓸 script machine 생성(슈퍼유닛아님) (JellyPanel)
   1. 연결까지
2. Jelly Panel 내에 Page Left Btn에 Onclick추가
   1. 객체 Jelly Panel
   2. 함수 script machine > trigger unity event
   3. Page Down
3. Page Right Btn도 대칭되게 끔 동일
4. Script machine으로 돌아와서
5. UnityEvent유닛부터 시작
6. Page Up Down에 해당하는 변수를 추가
   1. Scene급에서 Page 생성(int)
7. UnityEvent가 발생했을때
   1. 값을 가져와서
   2. 조건을 검사한 후
   3. 값을 재 세팅해준다.
   4. ![[Pasted image 20240801093252.png]]

#### 페이지 연동

1. 새로운 기법인 _BroadCasting_ 기법 사용 예정
2. 방금 만든 곳 뒤에 Trigger Custom Event 라는 유닛 추가
   1. 이벤트 이름은 Change
3. Jelly Panel > UnlockGroup > Icon 으로 이동(하이라키)
   1. 이 객체에 임베드 script machine을 추가
   2. Custom Event 유닛추가
   3. Jelly Panel 객체를 Script Graph 내로 드래그
   4. Manager, jelly sprtie list 필요, index 로 page 사용 (get list item 필요)
   5. ![[Pasted image 20240801095009.png]]
   6. 전체를 복사
4. Jelly Panel > Unlcok Group > Name Text로 이동
   1. 동일하게 임베드 script machine을 넣어주고
   2. 기존의 것 삭제 그리고 붙여넣기
   3. 리스트를 replace로 jelly name list를 가져오게 한다.
   4. set sprite를 set text로 replace한다.
   5. ![[Pasted image 20240801095333.png]]
5. Jelly Panel > Unlcok Group > Button > Text 로 이동
   1. list는 get jelly gold list
   2. {0:n0}을 써서 세자리마다 쉼표필요
   3. ![[Pasted image 20240801095702.png]]
   4.
6. Jelly Panel > Page Text 로 이동
   1. page에 1을 더하고(1부터 시작)
   2. format 내에서는 #{0:00}(항상 두자리)을 사용
   3. ![[Pasted image 20240801100018.png]]
7. 테스트
   1. 로직은 정상
   2. sprite 별로 이미지 크기가 달라서 조금 깨질 수 있음 => 처리 계획
8. Jelly Panel > UnlockGroup > Icon 의 임베드된 script machine으로 와서
   1. 끝부분 이후 set native size 유닛을 추가 후 연결
   2. 이미지 크기 문제는 해결
9. 젤리 판넬을 처음 열었을 때 정보를 불러오지 않아서 999,999가 찍히는 문제
   1. Jelly Panel로 가서
   2. Event > lifecyle > start 유닛을 추가후 이후 Cuttom 이벤트로 연결
   3. ![[Pasted image 20240801100702.png]]
10. 해금에 관한 정보들을 담고 있는 변수들이 없어서
11. Saved 급에 JellyUnlockList를 만들어 줌
    1. 자료형에 list를 쓸수가 없음 (유니티가 막음)
    2. 컴파일 빌드가 안됨
    3. 그래서 aot list를 사용함
       1. aot : 목표 플랫폼과 상관없이 중간언어 형태로 배포하는 방식
    4. ![[Pasted image 20240801101238.png]]
    5. 12개가 필요
12. Jelly Panel > Lock Group 에 Embed script machine 을 추가
    1. Lock Group내에 비활성화된 Lock Group을 script machine 내로 드래그 후
    2. set active를검색해서 넣는다.
    3. boolean 값을 반대로 하기위해서는 negate라는 유닛을사용한다.
    4. ![[Pasted image 20240801101814.png]]
13. Unlock Group 내에 Icon scipt machine 내용을복사해서
    1. lock group 내에 icon script machine에 붙여넣기
    2. 이 곳은 내용이 정확히 동일해서 수정 불필요
14. Unlock Group > Button > Text의 scipt machine 내용을복사해서
    1. lock group > Button > Text에 script machine에 붙여넣기
    2. list 는 get jelly jelatin list로 변경, 나머지는 동일
    3. 같은 곳에 on enable 유닛을 추가
       1. 비활성화된 상태엔 이벤트를 들을 수 없으므로
    4. ![[Pasted image 20240801102535.png]]
15. 위의 Icon도 on enabled 추가
    1. ![[Pasted image 20240801102832.png]]
16. 테스트

#### 해금 시스템

1. Jelly Panel > Lock Group >Lock Group > (Unlock) Button 의 Onclick 추가
   1. 객체 Jelly Panel
   2. ScriptMachine.TriggerUnityEvent
   3. Unlock
   4. ![[Pasted image 20240801104332.png]]
2. jelly panel의 script machine으로 돌아와서
   1. 기존 재화와 가격의 값을 가져와서 조건 검사
   2. 조건 성공시 차감 갱신로직과 boolean값 바꿔준후 UI 갱신필요
   3. ![[Pasted image 20240801104427.png]]
   4. ![[Pasted image 20240801104445.png]]
3. 테스트 전
   1. saved 급의 Intial을 100 200 unchecked로 하고
   2. saved쪽에 저장된 값들을 삭제?
   3. 테스트
4. Page Left Btn과 Page Right Btn 의 Navigation 값을 None으로 변경해준다.
5. 하이라키창 맨위에 검색하는 부분에을 보면 type으로 검색이 가능하다.

### 젤리 키우기 게임 - 구매 시스템 만들기 [V14]

#### 젤리 프리펩

1. jelly 밑의 shadow에 반영할 script machine을 생성
2. 그림자는 부모객체의 젤리의 ID에 따라서 달라져야 하므로
3. 부모 객체에 접근하기 위한 (transform)getParent 유닛
4. Switch 문법같은 효과를 내기위해서
   1. Select on Integer 유닛을 추가
   2. inspector에서 원하는 case 추가 가능
   3. 그림자는 젤리의 자식 객체이기때문에 set localposition을 사용
   4. ![[Pasted image 20240801121046.png]]
5. 프리펩 등록
   1. Assets내에 Prefabs 폴더를 만들어준다.
   2. 하이라키의 Jelly를 Prefabs 폴더내로 드래그 해준다.
   3. 등록 후 등록된 prefab이 position을 0,0,0인지 확인 후 초기화

#### 구매 시스템

1. Jell Panel > Unlock Group > Buy Button에 OnClick을 추가해준다.
   1. 객체 Jelly Panel
   2. Script Machin.TriggerUnityEvent
   3. string Buy
2. Jell Panel의 script machine 부분으로 와서
   1. Unlock 에 해당하는 두개의 그룹 복사
   2. 붙여넣기 후 값을 적절히 수정
3. 수정 내용
   1. Group name Unlock > Buy
   2. Check Jelatin > Check Gold
   3. list 를 Gold list로 변경
   4. ![[Pasted image 20240801125910.png]]
4. Buy : Success 쪽 추가 구현
   1. ![[Pasted image 20240801125825.png]]
   2. 젤리 생성을 위한 Instantiate 유닛 추가
      1. 오버로드 된것중 original position ratation을 매개 변수로 받는 것을 선택
      2. original에는 prefab인 jelly를 넣어준다.
      3. position은 set point(매크로)
      4. rotation은 identity를 검색하여서 기본값
   3. ![[Pasted image 20240801130606.png]]
5. 테스트
   1. 정상
   2. 문제점
      1. 다음 단계의 젤리를 생성시
      2. 최초등급의 젤리(녹색)만 나타남 +
      3. 종종 빨긴색에러로 GUI출력보다더많은 요청이있다라는 에러가 발생

#### 젤리 생성

1. Jelly.cs를 만들어준다.

   ```cs
   public class Jelly
   {
       public int ID;
       public int Level;
       public float Exp;

       public Jelly(int ID,int Level,float Exp){
           this.ID = ID;
           this.Level = Level;
           this.Exp = Exp;
       }
   }
   ```

1. Edit > Project Settings > Visual Scripting에서 Type Option 추가하고 아래 Regenerate Node누르기
1. ![[Pasted image 20240801131442.png]]
1. Jelly Panel의 Bu: Success 그룹으로 이동
   1. Jelly Create 유닛을 추가
   2. ![[Pasted image 20240801131902.png]]
   3. 이상태에서 중간로직인 슈퍼유닛 만들기(MakeJelly)
1. MakeJelly
   1. input output 필요
   2. 지역 변수를 쓸때에는 flow 변수로 미리 저장
   3. ![[Pasted image 20240801134307.png]]
   4. ![[Pasted image 20240801134319.png]]
   5. Set 그룹들의 컬러 Color 51149A
1. 다시 Jelly Panel로 가서 Make Jelly를 연결
   1. ![[Pasted image 20240801140007.png]]
   2. 레벨 초기값은 1로 정의했던것 주의
1. 테스트
   1. 플레이 후 saved의 값을 조정해서 잘되는지 테스트
   2. 정상
      1. 껏다키면 일부정보만 남고 젤리들이 초기화되는 문제

#### 젤리 관리

1. GameManager 객체 내에 GameManager(매크로)를 만들어준다.
   1. CustomEvent : 이름과 매개변수가 자유로운 볼트(비쥬얼 스크립트)만의 이벤트
      1. 유닛을 하나 추가해준다.
   2. Object 급으로 JellyList를 추가해준다. (Aot List)
      1. ![[Pasted image 20240801140750.png]]
   3. 리스트에 아이템을 넣을때 필요한것
      1. Collection > list > add list item
   4. 리스트에 아이템을 삭제할때는
      1. remove list item
   5. ![[Pasted image 20240801141138.png]]
2. 다시 MakeJelly 슈퍼유닛으로 이동
   1. MakeJelly내의 모든 세팅이 끝난후 Add trigger 작동
   2. ![[Pasted image 20240801141951.png]]
3. Jelly state machine으로 가서
   1. Pick > Idle로 넘어가는 trigger event 내에서
   2. Sell 그룹중
      1. trigger custom event sell 필요
      2. ![[Pasted image 20240801142554.png]]
4. 테스트
   1. 정상

#### 젤리 저장

1. Saved 급에 JellyList변수를 만들어준다.
2. 게임매니저에서의 JellyList는 GameObject 자료형이지만
3. Saved에서의 JellyList는 Jelly 타입으로 넣을 예정
4. 슈퍼 유닛 만들기(SaveJelly)
   1. input output 필요
   2. 일단 Saved에 잇는 jellylist를 가져옴
   3. Collection 내에 clear list가 있음
   4. control 내에서 for each loop가 있음
   5. ![[Pasted image 20240801194936.png]]
   6. ![[Pasted image 20240801194947.png]]
5. GameManager graph로 온다.
   1. 게임매니저에서 리스트 추가, 삭제시 젤리저장 슈퍼유닛 사용
   2. ![[Pasted image 20240801200148.png]]
6. 테스트
   1. ![[Pasted image 20240801200528.png]]
   2. Saved 내에 저장된 것이 보이지만 사용자 지정 자료형의 값은 보이지 않는다.
7. GameManager on Start에서 load기능을 구현
8. ![[Pasted image 20240801200945.png]]
9. 위코드를 실행하면 에러가 발생하는데
   1. forEach문 작동중에 list에 변형이되면 에러가 발생한다.
10. Scene급의 isStart 변수를만들어주고 (boolean false)
    1. 이 값을 활용한다.
    2. ![[Pasted image 20240801202130.png]]

#### 자동 재화 획득

1. Trigger Unity Event 유닛을 사용
   1. Auto
   2. 사용 위치는 GameManger On start 캐스팅중에 트리거 발동
2. 이하 UnityEvent Auto유닛에 graph inspector 창에서 coroutine을 체크해준다.
3. 무반 반복을 위해 While 유닛 추가
4. Wait for second 유닛 추가
5. get jellatin의 input에 매개변수가 필요하게됨 Data input을 추가해줌
   1. ![[Pasted image 20240801203618.png]]
6. 전에 GetJelatin을 사용한 부분도 같이 수정이 필요함
   1. Jelly 의 Touch State내에서 this가 필요
   2. ![[Pasted image 20240801203728.png]]
7. SaveVariable : Saved 변수를 PlayerPrefs에 반영
   1. ![[Pasted image 20240801204523.png]]
8. 테스트

### 젤리 키우기 게임 - 업그레이드 시스템 구현 [V15]

#### UI 구축하기

https://www.youtube.com/watch?v=bmcBdxWdTbs&list=PLO-mt5Iu5TeZA0y889ZMi9wJafthif03i&index=9

1. Plant Panel 내에서 빈객체 추가(Num Group)
   1. 앵커 전체크기
   2. Right 45
2. Num Group 내에 Image 추가
   1. 이미지 소스 icon 6
   2. set native size
   3. pos y 12
3. Num Group 내에 Text 추가
   1. 가로 세로 0 0
   2. overflow overflow
   3. 중앙 정렬,중앙 정렬
   4. 폰트 크기 7
   5. 도현체
   6. 라벨 젤리 아파트
4. Text 복사(Sub Text)
   1. pos y -6
   2. 폰트 크기 5
   3. 색상 8f9396
   4. 라벨 젤리 수용량 2
5. Jelly Panel > Unlcok Group > Buy Button을 복사하여 Num Group 아래로 가져온다.
   1. 앵커를 아래쪽 꽉찬것으로 변경
   2. left 2 right 2 pos y 3
6. Num Group을 복사 (Click Group)
   1. right 0 left 45
7. Click Group > Image
   1. Icon 7
8. Click Group > Text
   1. 라벨 젤리 꾹꾹이
9. Click Group > Sub Text
   1. 라벨 클릭 생산량 x 1

#### 데이터 만들기

1. Saved급의 변수 만들기
   1. NumLevel (int 1)
   2. ClickLevel(int 1)
2. GameManager.cs 내에 배열 변수 선언
   ```cs
       public int[] numGoldList; // 아파트 구매 골드비용
       public int[] clickGoldList; // 클릭 효율 구매 골드비용
   ```
3. 값을 넣어준 후 regenrate nodes

#### 데이터 연동

1. PlantPanel 객체에 script machine 추가 (PlantPanel.asset)
2. JellyPanel 내에 Check Gold 그룹을 복사해와서 붙여넣기
3. PlantPanel > Num Group > Button에 있는 Onclick을 적절히 교체
   1. 객체 PlantPanel
   2. 함수 ScriptMachine.TriggerUnityEvent
   3. String Num
4. Click Group > Button에도 유사하게 적용
   1. 똑같이 만든후
   2. String 만 Click으로 변경
5. 가격을 가저오는곳에서 list가
   1. get NumGoldList이다.(replace 필요)
6. Page가 아닌 NumLevel로 변경
   1. ![[Pasted image 20240801223405.png]]
   2. ![[Pasted image 20240801222457.png]]
7. Num Group > sub text에 embed된 script machine 추가
   1. ![[Pasted image 20240801221708.png]]
8. Num Group > Button > Text 에도 embed 된 script machine 추가
   1. 기존의 버튼을 복사해 왔기에 어느정도 값이 잇는데 부분변경을해준다.
   2. Panel이 Plant Panel 이여야하고
   3. Page 대신에 NumLevel
   4. list는 NumGoldList여야 한다.
   5. ![[Pasted image 20240801223541.png]]
9. 테스트
   1. numLevel이 최고 상태일때 예외 처리 필요
   2. Num gold list 와 click gold list 의 6번째 인덱스를 추가하고 0으로 값을 넣어준다.
10. Num group > Button 에 embed인 script machine 추가
    1. ![[Pasted image 20240801224536.png]]
11. click group > sub Text
12. ![[Pasted image 20240802075712.png]]
13. click group > button 에도비슷한 embed script machine을 추가한다.
    1. ![[Pasted image 20240801224707.png]]
14. click group > button > Text
    1. ![[Pasted image 20240802090430.png]]
15. Plant Panel의 script machine 내로 와서
    1. 위에 로직과 흡사하므로 복붙으로 빠르게 만들수있지만 유니티 낭비이므로
    2. 올바른방법으로 만든다.
16. Plant Panel
    1. select on flow 사용
    2. select on string 사용
    3. flow 변수에 값저장
    4. 어떤 타입의 업그레이드인지
       1. type이라는 이름을 flow급에 저장
       2. ![[Pasted image 20240802082223.png]]
    5. type에 따라서 올바른 리스트를 조회해오고 조건검사
       1. ![[Pasted image 20240802081743.png]]
    6. 증가 반영
       1. ![[Pasted image 20240802081822.png]]
    7. UI갱신을 위한 broadcasting
       1. ![[Pasted image 20240802081837.png]]
17. 테스트 전 준비
    1. saved 값 gold를 충분히 높게
    2. NumLevel과 ClickLevel은 1로 시작
18. 테스트
    1. 정상

#### 로직 구현

UI는 바뀌지만
내부 로직은 적용되지 않은 상태 (유닛 수량 제한 로직 추가 필요)

1. Jelly Panel script graph에서
2. Collection > count item 유닛이 있음
3. 젤리 수의 제한과 현재 젤리 수를 비교후 구매가능하도록
4. Buy:CheckGold와 Buy : Success 사이에 Buy : Check Num Level을 추가한다.
   1. ![[Pasted image 20240802085319.png]]
   2. ![[Pasted image 20240802085327.png]]
   3. ![[Pasted image 20240802085337.png]]
5. 테스트
   1. 구현한 곳까지 정상작동하지만
   2. _UX가 부족하다 (추가 구매가 불가능한 이유를 유저가 알 수 없음)_
      1. 추후 수정
6. 두번째 업그레이드 ClickLevel에 따른 효율적인 farming이 되도록 로직 추가 구현
   1. GetJelatin으로 이동
   2. 중간에 multiple을 한번더해서 clickLevel을 반영해준다.
   3. ![[Pasted image 20240802090102.png]]
   4. ![[Pasted image 20240802090112.png]]
7. 테스트
   1. 정상

### 젤리 키우기 게임 - ⚙️사운드와 옵션 시스템 [V16]

#### 사운드 매니저

1. 빈 객체를 만들어준다.(SoundManager)
   1. 초기화
2. SoundManager내부에 빈객체를 더 만들어준다.(Bgm Player) 3. 오디오 소스 컴포넌트추가 4. bgm을 clip에 넣어준다. 5. Play on awake 켜주고 6. Loop를 체크 해준다.
3. Bgm Player를 복사해준다.(Sfx Player)
   1. clip 제거
   2. Play on awake를 꺼준다.
   3. Loop를 체크해주 해준다.
4. SoundManager의 script machine 내에 Object급으로 두개의 플레이어를 추가해준다.
   1. 이름은 BgmPaleyr / SfxPlayer
   2. 자료형 AudioSource
   3. 드래그 드랍으로 넣어주기
5. SoundManager의 script machine으로 가서
   1. 컴스텀 이벤트 사용
      1. 매개변수 1개
   2. 이후 받아서 switch 처럼 처리하기 위하여
      1. select on string 사용
         1. select같은경우는 default가 비면안됨
      2. inspector에서 10가지 케이스를 처리할수잇게 추가
   3. clip이 있는 경로까지 Asset 에서 접근해서 script machine 내로 드래그 드랍해준다.(연결)
   4. ![[Pasted image 20240802110928.png]]
6. 사운드 매니져도 게임 매니저처럼 Scene급 변수에 저장하고 사용할 예정
   1. Sound / game object /당겨서 놓기

#### 사운드 적용

1. 여러번 사용해야하기 때문에 슈퍼유닛으로 다루기
2. 슈퍼 유닛 생성(PlaySound)
   1. input output 만들어주기
   2. input 쪽에 Name 이라는 이름으로 string을 추가해주고
      1. has Default value를 체크해주면 더 편하게 사용이 가능하다.
   3. ![[Pasted image 20240802104143.png]]
3. 이제 곳곳에 이 슈퍼유닛을 적용
   1. Jelly > Touch state > GetJelatin 이후
      1. ![[Pasted image 20240802104535.png]]
   2. set Exp 슈퍼유닛 내에서 ChangeAc 이후
      1. ![[Pasted image 20240802104738.png]]
   3. GameManger의 Add jelly 그룹 끝 과 Remove Jelly 그룹 끝
      1. ![[Pasted image 20240802104958.png]]
      2. ![[Pasted image 20240802105003.png]]
   4. Jelly Panel에서 unlock Success에서 broadcasting 전에
      1. ![[Pasted image 20240802105242.png]]
   5. unlock Success로 넘어가기전의 if문의 false쪽에서
      1. ![[Pasted image 20240802105338.png]]
   6. 밑에 있는 buy : check gold 에서도 동일
   7. Plant Panel에서도 Buy : Check Gold 그룹의 if문 false쪽에서 도 동일
   8. 우측의 broadcasting전에 unlock sound play
      1. ![[Pasted image 20240802105732.png]]
   9. Button Panel에 Change State 이후에 Button play
      1. ![[Pasted image 20240802105954.png]]
   10. ButtonOption내에서
       1. ![[Pasted image 20240802110206.png]]
   11. JellyPanel에 Page Change 할때 Button 사운드 추가
       1. Set Page 후 broadcasting 전
   12. ![[Pasted image 20240802110357.png]]

#### 옵션 UI 구축

1. Option Panel을 열어보면 기존의 Panel이라는 이미지만 있다.
2. Panel 이하에 빈객체 추가(Sfx Group)
   1. 앵커 위쪽에 꽉 차게
   2. left 2 right 2 pos y -5 hieght 5
3. Sfx Group 내에 Text 추가(UI Text)
   1. 앵커 좌측에 붙여주기
   2. left 1
   3. 가로 세로 0 0
   4. overflow overflow
   5. 좌측정렬, 중앙정렬
   6. 도현체
   7. 라벨 효과음
4. Sfx Group 내에 slider 추가(Sfx Slider)
   1. 앵커 전체크기
   2. left 21 right 5
   3. Normal color 색상 0BCFC4
   4. HighLight 색상 48eae1
   5. Pressed 색상 00a5a9
   6. selected 색상 00a5a9
   7. value 0.5
5. Sfx Slider > fillArea
   1. 앵커 전체크기
   2. value 새로고침
6. Sfx Slider > fillArea > Fill 이라는 객체
   1. 색상 0BCFC4
   2. 소스 이미지 Panel
   3. Image Type > simple
   4. set native size
   5. Image Type > sliced
   6. 앵커 전체크기
7. Sfx Slider > background
   1. 소스 이미지 Panel
   2. Image Type > simple
   3. set native size
   4. Image Type > sliced
   5. 앵커 전체크기
8. Sfx Slider > Handle slide Area > Handle
   1. 소스 이미지 Panel 2.
   2. Image Type > simple
   3. set native size
   4. Image Type > sliced
   5. 앵커 상하만 가득차게
   6. pos x -5 가로 10
   7. top -2 bottom -2
9. Sfx Slider > Handle slide Area
   1. left 5 right -5
10. Sfx Group 복사 (Bgm Group)
    1. pos y -15
    2. 배경음
11. Panel에 버튼하나 추가(Resume Button)
    1. 앵커 아래쪽에 꽉 차게
    2. 이미지 소스 panel
    3. left 3 right 41 pos y 3 height 12
12. Resume Button > Text
    1. 앵커 중앙
    2. 폭과 높이는 0
    3. overflow,overflow
    4. 도현체
    5. 폰트 크기 6
13. Resume Button 복사 (Exit Button)
    1. left 41 right 3
    2. Normal color FF6969
    3. Highlighted ff9e9e
    4. Pressed d93737
    5. Selected d93737
14. Exit Button > Text
    1. 라벨 나가기
    2. 색상 fffff

#### 로직 구현

1. Saved 급의 변수를 만들어준다.
   1. Sfx float 0.5
   2. Bgm float 0.5
2. sfx slider에 embed script machine 추가
   1. ![[Pasted image 20240802123241.png]]
3. bgm Slider에도 비슷하게 만들어주기
   1. ![[Pasted image 20240802123426.png]]
4. SoundManager에서
   1. ![[Pasted image 20240802123814.png]]
5. ButtonOption에서 Hide라는 유니티 이벤트가 발생하면 창 숨기도록 연결
   1. ![[Pasted image 20240802132632.png]]
6. Option Panel에서 Resume Button의 OnClick에 메서드추가
   1. 객체 Option Button
   2. scriptmachine.triggerUnityevent
   3. string Hide
7. Option Panel에서 Exit Button에는 별도의 embed script machine 추가
   1. Timer 유닛내의
      1. Unscaled: Time Scale에 영향을 받지 않도록 해주는 옵션
   2. ![[Pasted image 20240802140400.png]]
8. Option Panel 비활성화
9. 테스트
   1. 정상
   2. 변형
      1. sfx slider / bgm slider > Handle slide Area
         1. left 5 right 5
         2. handle pos x 0

###
