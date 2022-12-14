using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*옵저버 패턴
 * 
 * 옵저버 패넡은 한 객체가 주체 역할을 하고 다른 객체가 관찬ㄹ자 역할을 맡는 객체 간의
 * 일대다 관계를 설정하는 것이 핵심 목적이다. 주체 역할을 맡는 객체는 내부에서 변경되었을 때
 * 관찰자에게 알리는 책임을 진다. 이는 객체가 특정 이벤트 알림을 구독하고 수신하는 게시자와
 * 구독자의 관계와 유사하다. 핵심 차이점은 옵저버 패턴에서는 주체와 관찰자가 서로를 알고 있어
 * 가볍게 결합된다는 점이다.
 * 
 * 중요한점 : 
 *  - AttachObserver() : 알림받을 관찰자 목록에 객체를 추가한다.
 *  - DetachObserver() : 관찰자 목록에서 관찰자를 없앤다.
 *  - NotifyObservers() : 주체의 관찰자 목록에 추가된 모든 객체에 알림을 보낸다.
 *  - 관리자 역할을 맡는 객체는 NOtify()라는 공개 메서드를 구현해야 한다. 
 *     주체 객체의 상태가 변경되었을 때 알리고자 사용한다.
 *     
 *  장점
 *  - 역동성 : 주체에 필요한 만큼의 객체를 관찰자로 추가할 수 있으며 런타임에 동적으로 제거할 수도 있다.
 *  - 일대다 : 옵저버 패턴의 주요 이점은 일대다 관계가 있는 객체 간 이벤트 처리 시스템의 구현 문제를 
 *                우아하게 해결한다는 점이다.
 *  단점
 *  - 무질서 : 옵저버 패턴은 관찰자가 알림받는 순서를 보장하지 않는다. 둘 이상의 옵저버 객체가 종속성을 
 *                공유하고 특정 순서에 맞춰 함께 작동해야만 한다면 기본 형태의 옵저버 패턴은 적당하지 않다.
 *                기본 옵저버 패턴은 이런 종류의 실행을 다루도록 설계되지 않았다.
 *  - 누수 : 주체는 관찰자에 대한 강한 참조를 가져 메모리 누수를 일으킬 수 있다. 제대로 분리 및 삭제되지 않았는데 
 *             패턴을 잘못 구현하고 관찰자 객체가 더 이상 필요하지 않은 경우 가비지 컬렉션에서 문제를 일으킬 수 있으며
 *             일부 리소스는 해제되지 않는다.
 * 
 * 
 */