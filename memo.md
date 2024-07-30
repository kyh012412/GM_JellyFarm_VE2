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

###
