import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/schedule/domain/entities/subject_entity.dart';
import 'package:studenda_mobile/model/common/mark.dart';
import 'package:studenda_mobile/resources/colors.dart';

final marks = <Mark>[
  Mark(0, "12.03 13:15 (неделя: син)", "+"),
  Mark(1, "19.03 13:15 (неделя: красн)", "-"),
  Mark(2, "12.03 13:15 (неделя: син)", "-"),
  Mark(3, "19.03 13:15 (неделя: красн)", "+"),
  Mark(4, "19.03 13:15 (неделя: красн)", "-"),
  Mark(5, "12.03 13:15 (неделя: син)", "+"),
];

class JournalAttendanceScreenWidget extends StatelessWidget {
  final SubjectEntity subject;

  const JournalAttendanceScreenWidget({super.key, required this.subject});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp),
          color: Colors.white,
          onPressed: () => {Navigator.of(context).pop()},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: Text(
          subject.name,
          style: const TextStyle(color: Colors.white, fontSize: 25),
        ),
        actions: [
          IconButton(
            onPressed: () => {Navigator.of(context).pushNamed('/notification')},
            icon: const Icon(
              Icons.notifications,
              color: Colors.white,
            ),
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(14.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Column(
                children: marks
                    .map((element) => _MarkItemWidget(mark: element))
                    .toList(),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class _MarkItemWidget extends StatelessWidget {
  final Mark mark;

  const _MarkItemWidget({required this.mark});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5),
      child: SizedBox(
        height: 50,
        child: Container(
          decoration: BoxDecoration(
            border: Border.all(
              color: const Color.fromARGB(255, 170, 141, 211),
            ),
            color: Colors.white,
            borderRadius: const BorderRadius.all(Radius.circular(5)),
          ),
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16.0),
            child: Row(
              children: [
                Expanded(
                  child: Text(
                    mark.date,
                    style: const TextStyle(
                      color: mainForegroundColor,
                      fontSize: 18,
                    ),
                  ),
                ),
                Text(
                  mark.value,
                  style: const TextStyle(
                    color: mainForegroundColor,
                    fontSize: 20,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
