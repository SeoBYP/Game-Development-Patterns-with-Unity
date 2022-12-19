using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*서비스 로케이터 
 * 
 * 서비스 로케이터 패턴은 간단하면서 효율적인 패턴이다. 서비스 로케이터 패턴은 초기화된 종속성의 중앙 레지스트리를 
 * 갖는 것이 핵심이다. 더 정확하게 말하자면 이런 종속성은 "서비스 계약"이라는 인터페이스로 노출될 수 있는 특정 서비스를 
 * 제공하는 구성 요소다. 클라이언트가 특정 서비스를 호출해야 할 때 현지화 및 초기화 방법을 몰라도 된다. 서비스 로케이터 패턴에 
 * 물어보면 서비스 계약을 이행하는 데 필요한 모든 작업을 수행할 수 있다. 매우 단순한 디자인이며 구현하디 쉽다.
 * 
 * 전통적인 패턴과 비교했을 때 서비스 로케이터 패턴의 전반적인 디자인은 이론적이지 않고 매우 실용적이다. 클라이언트에 대한 서비스를 찾는 
 * 것이 목적이다. 특정 서비스를 제공하는 객체의 중앙 레지스트리를 유지해 달성한다. 서비스 로케이터 패턴은 클라이언트(요청자)와 서비스 
 * 제공자 사이의 프록시 역할을 하며 어느 정도 클라이언트와 서비스 제공자를 분리한다. 클라이언트는 해결해야 하는 종속성이 있고 서비스에 
 * 접근해야 할 때만 서비스 로케이터 패턴을 호출한다. 서비스 로케이터 패턴은 고객에게 주문을 받고 레스토랑이 고객에게 제공하는 다양한 
 * 서비스의 중개자 역할을 하는 웨이터와 유사하다.
 * 
 * 장점 
 * - 런타임 최적화: 런타임 콘텍스트에 따라 특정 서비스를 완료하고자 더 최적화된 라이브러리나 컴포넌트를 동적으로 감지하여 응용프로그램을
 * 최적화할 수 있다.
 * - 단순성 : 구현하기 가장 간단한 종속성 관리 패턴 중 하나다. 종속성 주입(Dependency Injection,DI) 프레임워크의 가파른 학습 곡선이 없다.
 * 프로젝트에서 빠르게 사용할 수 있으며 동료에게 패턴을 가르쳐줄 수 있다.
 * 
 * 단점
 * - 블랙박스화 : 서비스 로케이터 패턴의 레지스트리는 클래스 종속성을 읽기 어렵게 한다. 종속성을 잃거나 잘못 들록했다면 컴파일이 아닌 
 * 이슈가 발생할 수 있다.
 * - 전역적 종속성 : 잘못된 목적으로 남용하면 서비스 로케이터 패턴 자체가 관리하기 힘든 전역 종속성이 될 수 있다. 코드는 과하게 의존하게 
 * 되고 나머지 핵심 구성 요소에서 쉽게 분리할 수 없게 된다.
 * 
 * 서비스 로케이터 패턴은 동적으로 접근해야 하는 서비스 목록이 있지만 서비스를 얻는 것과 관련된 과정을 캡슐화하고 싶을 때 사용한다.
 * 서비스 로케이터 패턴을 사용할 때 고려해야 할 도 다른 측명은 사용하지 않는 경우다. 보통 전역적으로 접근할 수 있어 서비스를 찾고 접근할 
 * 수 있도록 해야 한다. 그 다음 전역 범위를 가진 서비스를 노출하는 데만 사용해야 한다. 
 * 
 */
