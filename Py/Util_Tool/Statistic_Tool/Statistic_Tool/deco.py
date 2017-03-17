class Verbose:
	def __init__(self, f):
		print "Initializing Verbose."
		self.tt = f;

	def __call__(self):
		print "Begin", self.tt.__name__
		self.tt();
		print "End", self.tt.__name__


def my_function():
    print "hello, world."


print "Program start"
ver = Verbose(my_function)
ver.__call__()






