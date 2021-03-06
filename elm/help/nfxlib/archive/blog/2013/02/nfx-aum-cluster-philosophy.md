# NFX / Aum Cluster - The Philosophy

## The Problem

The cost of distributed cloud systems development has skyrocketed. Many companies decided to use simple languages like PHP and Ruby that facilitate quick Web site construction but are not suitable for high-computational loads facing big problems down the road (i.e. PHP@Facebook). To this day there is no unified way for organizing/operating clusters of server nodes which would have been as standardized as C APIs for IO tasks. It is currently impossible to create and operate/manage a system instance with features listed below without significant resources / big staff.

There are many cloud-system offerings, however none of them really fit the purpose in my perspective. Those systems can be very broadly be categorized as one of the following:
* Hardware/Infrastructure management and/or IaaS (Infrastructure as a Service) - Amazon, Mesos (for cluster management), Azure
* Cloud-based "all-done-for-you" PaaS (Platform as Service) - Google App Engine etc.

Why did I subdivide those numerous offerings this way? Because in my experience, it is either bare metal/OS management that they do for you OR a whole special kind of cloud OS that you need to code against from scratch (i.e. Google App Engine). I can not run Google App Engine locally at home, neither can I use it for a client-server app that my local client wants to have installed in his store. Furthermore, the skill set that is required is completely different from the one many client/server/web-developers have.

In real life it is very hard to build a simple cloud/highly-scalable system. Those offering are great when you read the tech papers but really have very many limitations. For example, Google App Engine requires really special kind of architecture to operate in. One can not take code away from Google App Engine and port it to Azure. Azure, on the other hand, mostly gives you physicial boxes - and **some** services (like special kind of SQL server) but that is it. .NET framework does not have concepts/classes that would promote the creation of cluster software (I am not saying that it should). One can not take a console app in C# and run it "in the cloud", because **there is no well-defined stipulation of what "run in the cloud" means**.


## Why Many Startups Face Troubles 2 Years Later?

Many companies/startups elect languages like Python/Ruby or PHP because those languages have a convenient set of libraries allowing them to put some working sites in production very quickly. The problem, however, is that later, when the number of users increases and business requirements start to demand more intense logic, those solutions fail for number of reasons:
* Usually, the lack of proper architecture of the system as a whole. No consideration may have been given from day one to concerns like user geo affinity, distributed caching, session state management, common configuration formats, logging, instrumentation, management of large installations with many nodes. Developers usually do not consider:
  * That any process (be it web server, app server, tool etc.) need to be remotely controlled in cluster environment so it can be stopped/queried/signaled
  * All tools must be command-line enabled (not UI only), so they can be scripted and executed in an unattended fashion
  * There may be 100s of computers to configure, instead of 1. Are we ready to maintain 100s of configuration files?
  * Time zones in distributed systems, cluster groups, NOCs. Where is time obtained from? What time zone? What time shows in global reports?
  * Any UI element on the screen may be protected by permission (i.e. “Post Charges” button may not show for users who do not have access)
  * Row-based security. Security checks may span not only screens/functions but also some data entities such as rows
  * Web session state may not reside locally (i.e. local disk/memcache) if user reconnects to a different server in the cluster
  * Pushing messages to subscribers/topics. Using appropriate protocols (i.e. UDP). Not thought about when the whole system runs from one web server.
* Most startups use one central database instance (which is convenient to code against), and have big troubles when they need to split databases so they can scale, because all code depends on central data locations (one connect string used in 100s of classes)
* The scripting languages (e.g. PHP, Python, Ruby) used for web site implementation are not performant enough for solving general programming problems (try to build a PHP compiler in PHP) involved in high-throughput processing. It is slow for such tasks and was never meant to be used that way. What happens next, is that developers start to use C++ or C where the development paradigm is absolutely incompatible with the one in PHP, complexity keeps increasing as the number of standards internal to the system is increased. You need more bodies to develop this way
* Security functionality is usually overlooked as well as most applications do not have security beyond user login and conditional menu display on the homepage which depends on 5-10 fixed role checks. Later, businesses need to start protecting individual screens/UI elements with permissions. This usually creates mess in code and eventually precipitates a major re-write. The inter-service security in the backend is usually completely overlooked so any node can call any other node bypassing all checks.
* The ALM (Application LifeCycle Management) is usually not really though about. “We will deploy and manage changes somehow when we come to it”


## Our Vision

We have been dealing with these problems for the past 20 years. We came to realization as far back as 1996 that there is a need for a “Business Operating System”. The main idea is to have a lego-like kit of micro solutions that are very configurable and allow developers to assemble complex systems in no time as the majority of system/architecture/ALM-specific challenges will be solved for you.

When you use Linux or Windows you do not need to understand how files are written to disk. You don’t need to know how video card works. OSes do a great job, but when it comes to business/data-centric apps - there is no similar approach.

These days systems are very much distributed and back-end/cloud based. Inherently, there are many nodes/servers to run your system on, hence we came up with our “Clusterware” concept. One may say that Hadoop does just that, but this is only a slice of Aum Cluster/NFX.

We try to address the whole wide array of different problems, whereas Hadoop mostly concentrates on job orchestration, we do that just as one of Aum Cluster functions.

In a nutshell, Aum Clusterware is a software system that promotes multi-layered/tiered architecture with central coordinator service bus and accompanying set of libraries (less than 5 mbytes total) that unify/provide:
* Central admin control panel for any admin task
* Configuration of 10,000s of nodes
* Inventorization of nodes, services, endpoints, configurations, components, classes and even methods
* Deployment / Change Management in server farms. Component versioning and distribution
* Monitoring/Instrumentation/Logging/Alerts
* Job / Change scheduling
* Security/Permissions/Modules/Namespaces/Roles
* User profile management/migration. User grouping. OpenID/Twitter/FB integration
* Data Partitioning
* Workload balancing
* Contract-based service bus. Internal bus uses direct TCP/UDP/ZeroMQ for max throughput
* Async and Sync messaging and queuing
* Subscription / Notifications
* Charge processing
* Content Management
* Complex UI construction on Web, Mobile, Desktop purposely build to interact with Aum.RecordModel MVVM
* Support for SQL and NoSQL databases. Decoupling business logic from database
* Map:Reduce + scheduling for high volumes of data and/or computations
* Big memory - stateful web and server instances
* Full text search
* Global unique ID generation, obfuscation, validation and resolution
* Dynamic database partitioning. Home databases. Locality of reference
* Geo-aware data migrations and replication
* Social dashboards with profile integration: chat/voting
* C# code base, interoperable with C++ on Linux, Erlang for complex concurrency tasks


## The Philosophy

We want to reduce complexity but without feature loss. The way to achieve this effect is this - **reduce the number of standards used in the system**. The less standards we need to support/keep in mind/remember - the simpler the whole system becomes. For example: instead of supporting 4 different logging frameworks where one uses this kind of configuration and that uses a completely different set of configuration - we use just one. Once a developer reads logging tutorial he/she can easily understand how logging works in any tier of the whole system.

Another big thing - is runtime/language. I know that I will start a holy war here. Before reading further, please answer the following questions:
* What primary language/s are Windows and Linux (and others) written in?
* What language are you web browsers written in?
* What about databases? Oracle, MySQL, MsSQL, DB2?
* What about major desktop apps: Photoshop, Office, various Audio and Video editing tools?
* What about compilers/Interpreters for: C, C++, JavaScript, Java, C#, Ruby, PHP?

For some reason, none of the mission critical software like OS kernels and DB servers, compilers and web browsers are written in PHP or Ruby. It is not because of historical reasons. Has anyone written a new web browser in Erlang, Ruby or Python yet? Of course not.

This is all because Erlang, Ruby, Python, PHP (and 20 others) are specialized tools that simplify some particular aspects of the system architecture/coding, but they all SUCK BIG TIME when it comes to system programming. PHP is not a good tool for database server programming. Ruby was not meant to be used for writing high-performance compilers. So, to build a large sophisticated system one would use 25 different languages for different things. Because all of those disparate components require their own "gurus", configuration/operation standards we decided against those tools/languages.

That did it for us - we had to select a language which is really a general-purpose one, very suitable for system programming and creation of large-scale systems. When I say "system programming", I do not mean "device drivers" and "Linux kernel modules". What I do mean is this:
* Ability to implement custom data structures efficiently (trees, maps) with execution speed close or equal to C/C++
* 64 bit support - a must. Must be able to allocate 128Gb ram per process - with no sweat
* Process model that supports large ram and long execution times (months) without restarts (efficient GC)
* Process model that supports globals, context globals, threads and lite-weight concurrency models
* Efficient background/concurrent GC
* Ability to minimize GC load by using instance pools (or stack-alloced structs in CLR)
* Fast integer and floating point math. Efficient CPU-register Bit operations
* Good code modularization
* Good support for working with strings, especially Unicode compliance
* Good network stack support - TCP/IP/Sockets

Both JVM and CLR definitely qualify.

Of course no one can touch C and C++ when it comes to efficiency, custom memory allocators and pointer manipulation, but for one tiny defect. Those bare-bones languages are very low-level for the business/data-centric app creation. The lack of good reflection in C++ really kills it for our purposes. Had we taken C++, we would have needed to create a C++ frontend language that would have supported reflection, GC. So basically we would have had to create our own JVM+Java or CLR+C# which is not practical. Instead we decided to use existing CLR/C# or JVM/Java in such a way that our code does not depend on particular features of library bloat that surrounds those platforms, rather we have re-written all base service ourselves, thus bringing a "Unistack" to life.

UNI-STACK = a Unifed Stack of software to get your job done. Use one library instead of 25 3rd parties that all increase complexity. In Unistack everything is named, configured, and operates the same way, thus reducing complexity 10-fold. Unistack was **purposely coded to facilitate distributed apps creation**, yet (unlike many PaaS) it can be used to write basic client-server apps that all run on one machine without any bloat.

**Transaction processing: share nothing, scale horizontally**

**Configuration/management: share everything or as much as possible**

**Unify patterns, languages, components**

**Avoid 3rd parties as much as possible** - direct and transitive dependencies

**First: reuse**, Second: build using Aum/NFX, Third: use open source, Fourth: buy proprietary

---
Dmitriy Khmaladze  
February 3, 2013