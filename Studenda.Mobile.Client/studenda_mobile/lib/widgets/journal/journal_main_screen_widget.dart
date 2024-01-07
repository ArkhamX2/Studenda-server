import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/schedule/subject.dart';

import 'subject_item_widget.dart';

final subjects = [
  Subject(0, "Математика", "ВЦ-315", "QWERTY"),
  Subject(1, "Физкультура", "ВЦ-315", "QWERTY"),
  Subject(2, "Базы данных", "ВЦ-315", "QWERTY"),
  Subject(3, "Экономика", "ВЦ-315", "QWERTY"),
  Subject(2, "Базы данных", "ВЦ-315", "QWERTY"),
  Subject(3, "Экономика", "ВЦ-315", "QWERTY"),
];

class JournalMainScreenWidget extends StatefulWidget {
  const JournalMainScreenWidget({super.key});

  @override
  State<JournalMainScreenWidget> createState() =>
      _JournalMainScreenWidgetState();
}

class _JournalMainScreenWidgetState extends State<JournalMainScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      appBar: AppBar(
        titleSpacing: 0,
        automaticallyImplyLeading: false,
        centerTitle: true,
        title: const Text(
          'Журнал',
          style: TextStyle(color: Colors.white, fontSize: 25),
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
              crossAxisAlignment: CrossAxisAlignment.start,
              children: subjects
                  .map((element) => SubjectItemWidget(subject: element))
                  .toList()),
        ),
      ),
    );
  }
}
