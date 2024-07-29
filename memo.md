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
