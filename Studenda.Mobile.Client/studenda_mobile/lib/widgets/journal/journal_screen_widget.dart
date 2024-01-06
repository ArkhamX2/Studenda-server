import 'package:flutter/material.dart';

class JournalScreenWidget extends StatefulWidget {
  const JournalScreenWidget({super.key});

  @override
  State<JournalScreenWidget> createState() => _JournalScreenWidgetState();
}

class _JournalScreenWidgetState extends State<JournalScreenWidget> {
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
            onPressed: () => {Navigator.of(context).pushReplacementNamed('/notification')},
            icon: const Icon(Icons.notifications, color: Colors.white,),
          ),
        ],
      ),
      body: const Padding(
        padding: EdgeInsets.all(14.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text("Предмет1"),
              Text("Предмет1"),
              Text("Предмет1"),
              Text("Предмет1"),
            ],
          ),
        ),
      ),
    );
  }
}
