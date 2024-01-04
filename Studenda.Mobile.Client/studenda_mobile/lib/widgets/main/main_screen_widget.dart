import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';
import 'package:studenda_mobile/widgets/UI/button_widget.dart';

final List<Subject> schedule = <Subject>[
  Subject(0,"Математика","ВЦ-315"),
  Subject(1,"Физкультура","ВЦ-315"),
  Subject(2,"Базы данных","ВЦ-315"),
  Subject(3,"Экономика","ВЦ-315"),
];

class MainScreenWidget extends StatefulWidget {
  const MainScreenWidget({super.key});

  @override
  State<MainScreenWidget> createState() => _MainScreenWidgetState();
}

class _MainScreenWidgetState extends State<MainScreenWidget> {


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          onPressed: () => {},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Главная',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
              onPressed: () => {}, icon: const Icon(Icons.notifications),),
        ],
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Column(
                children: schedule.map((element) => _ScheduleItemWidget(subject: element)).toList(),
              ),

              
              StudendaButton(title: "Подтвердить", event: (){}),
            ],
          ),
        ),
      ),
    );
  }
}

class _ScheduleItemWidget extends StatelessWidget {
  static const List<String> lessonTimes = [
    "8:30\n10:05",
    "10:15\n11:50",
    "12:15\n13:50",
    "14:00\n15:35",
    "15:45\n17:20",
    "17:30\n19:05",
  ];
  final Subject subject;
  const _ScheduleItemWidget({required this.subject});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: IntrinsicHeight(
        child: Row(
          children: [
            Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Text(lessonTimes[subject.dayPosition], textAlign: TextAlign.right),
              ],
            ),
            const VerticalDivider(
              width: 20,
              thickness: 1,
              indent: 5,
              endIndent: 5,
              color: Colors.grey,
            ),
            Expanded(
              child: Container(
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(5),
                  border: Border.all(
                    color: const Color.fromARGB(60, 0, 0, 0),
                  ),
                ),
                child:Row(
                  children: [
                    Expanded(child: Center(child: Text(subject.name))),
                    Text(subject.place),
                    const SizedBox(
                      width: 14,
                    ),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
